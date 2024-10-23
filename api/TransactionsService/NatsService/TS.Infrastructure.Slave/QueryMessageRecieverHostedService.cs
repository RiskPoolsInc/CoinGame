using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client;
using Newtonsoft.Json;
using TS.Configuration;
using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Nats.Abstract;
using SilentNotary.Cqrs.Nats.Adapters;
using SilentNotary.Cqrs.Queries;

namespace TS.Infrastructure.Slave
{
    public abstract class QueryMessageRecieverHostedService : IHostedService
    {
        private readonly INatsSerializer _serializer;
        private readonly IDiScope _diScope;
        private readonly INatsConnectionFactory _connectionFactory;
        private readonly INatsReceiverQueryQueueFactory _queueFactory;
        private readonly ILogService _logService;
        private readonly ITypeFactory _typeFactory;
        private IEncodedConnection _connection;
        private IAsyncSubscription _subscription;

        public QueryMessageRecieverHostedService(INatsSerializer serializer, ITypeFactory typeFactory,
            IDiScope diScope, INatsConnectionFactory connectionFactory, INatsReceiverQueryQueueFactory queueFactory,
            ILogService logService)
        {
            _serializer = serializer;
            _diScope = diScope;
            _connectionFactory = connectionFactory;
            _queueFactory = queueFactory;
            _logService = logService;
            _typeFactory = typeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logService.LogInfo<QueryMessageRecieverHostedService>("Staring..");

            var disconnected = true;
            do
            {
                try
                {
                    _connection = _connectionFactory.Get<QueryNatsAdapter>();
                    disconnected = false;
                }
                catch (NATSNoServersException ex)
                {
                    _logService.LogError<QueryMessageRecieverHostedService>(ex,
                        "Cant connect NATS queue, retry in 10 sec..");
                    await Task.Delay(10000, cancellationToken);
                }
            } while (disconnected);

            _logService.LogInfo<QueryMessageRecieverHostedService>("Started!");

            _connection = _connectionFactory.Get<QueryNatsAdapter>();
            _subscription = _connection.SubscribeAsync(_queueFactory.Get(), CreateHandler());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logService.LogInfo<QueryMessageRecieverHostedService>("Stopped!");

            _connection.Dispose();
            _subscription.Dispose();
            return Task.CompletedTask;
        }

        private EventHandler<EncodedMessageEventArgs> CreateHandler()
        {
            return async (sender, args) =>
            {
                var queryAdapter = (QueryNatsAdapter) args.ReceivedObject;
                var typeArgs = queryAdapter.CriterionType.Split('|');
                var reply = typeArgs[1];
                
                var criterionType = _typeFactory.Get(typeArgs[0]);
                var queryResultType = _typeFactory.Get(queryAdapter.QueryResultType);
                try
                {
                    _logService.LogDebug<QueryMessageRecieverHostedService>(
                        $"accepted msg: {queryAdapter.CriterionType} {queryAdapter.QueryResultType}");

                    var queryOpenGenericType = typeof(IQuery<,>);
                    var queryClosedGenericType = queryOpenGenericType.MakeGenericType(criterionType, queryResultType);

                    var query = _diScope.Resolve(queryClosedGenericType);
                    if (query == null)
                    {
                        throw new Exception(
                            $"No handler found for {queryAdapter.CriterionType} {queryAdapter.QueryResultType}");
                    }

                    ICriterion criterion;
                    try
                    {
                        criterion = _serializer.DeserializeMsg<ICriterion>(queryAdapter.Criterion, criterionType);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(
                            $"Error when deserializing {queryAdapter.CriterionType} {queryAdapter.QueryResultType}",
                            ex);
                    }

                    var methods = query
                        .GetType()
                        .GetTypeInfo()
                        .GetDeclaredMethods("Ask");

                    bool contains = false;
                    foreach (var method in methods)
                    {
                        contains = method.GetParameters()
                                       .FirstOrDefault()
                                       ?.ParameterType
                                       .ToString()
                                       .Contains(criterionType.ToString()) == true
                                   && method.ReturnType == typeof(Task<>).MakeGenericType(queryResultType);

                        if (contains)
                        {
                            Console.WriteLine($"INFO - Query Handler Host - Execute - method: {method.Name} query: {queryAdapter.Criterion}");
                            _logService.LogInfo<QueryMessageRecieverHostedService>($" method: {method.Name} query: {queryAdapter.Criterion}");
                            var result = await method.InvokeAsync(query, criterion);
                            queryAdapter.QueryResult = JsonConvert.SerializeObject(result);
                            Console.WriteLine($"INFO - Query Handler Host - Executed - result: {queryAdapter.QueryResult} ");
                            break;
                        }
                    }

                    if (!contains)
                    {
                        throw new Exception("Resolver not found");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR - Query Handler Host - {ex.Message} \n\tstackTrace{ex.StackTrace}");
                    _logService.LogError<QueryMessageRecieverHostedService>(ex);
                    queryAdapter.QueryResultType = typeof(string).ToString();
                    queryAdapter.QueryResult = $"message: {ex.Message} \n\tstackTrace{ex.StackTrace}";
                }

                try
                {
                    _connection.Publish(reply, queryAdapter);
                    _connection.Flush();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR - Query Handler Host - Error when publish result: {ex.Message}");
                    _logService.LogError<QueryMessageRecieverHostedService>(ex, "Error when publish result");
                }
            };
        }
    }
}