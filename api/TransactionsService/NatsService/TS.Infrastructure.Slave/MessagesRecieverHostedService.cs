using System;
using System.Threading;
using System.Threading.Tasks;

using App.Interfaces.Core;

using CSharpFunctionalExtensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NATS.Client;
using Newtonsoft.Json;
using TS.Configuration;
using SilentNotary.Common;
using SilentNotary.Cqrs.Nats.Abstract;
using SilentNotary.Cqrs.Nats.Adapters;

namespace TS.Infrastructure.Slave
{
    public abstract class MessagesRecieverHostedService : IHostedService
    {
        private readonly INatsSerializer _serializer;
        private readonly IDispatcher _messageSender;
        private readonly INatsConnectionFactory _connectionFactory;
        private readonly ITypeFactory _typeFactory;
        private readonly INatsReceiverCommandQueueFactory _queueFactory;
        private readonly ILogService _logService;
        private IEncodedConnection _connection;
        private IEncodedConnection _responeConnection;
        private IAsyncSubscription _subscription;

        protected MessagesRecieverHostedService(INatsSerializer serializer, IDispatcher messageSender,
                                               INatsConnectionFactory connectionFactory, ITypeFactory typeFactory,
                                               INatsReceiverCommandQueueFactory queueFactory, ILogService logService)
        {
            _serializer = serializer;
            _messageSender = messageSender;
            _connectionFactory = connectionFactory;
            _typeFactory = typeFactory;
            _queueFactory = queueFactory;
            _logService = logService;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            _logService.LogInfo<MessagesRecieverHostedService>("Staring..");

            var disconnected = true;
            do
            {
                try
                {
                    _connection = _connectionFactory.Get<CommandNatsAdapter>();
                    _responeConnection = _connectionFactory.Get<ResultAdapter>();
                    disconnected = false;
                }
                catch (Exception ex)
                {
                    _logService.LogError<MessagesRecieverHostedService>(ex, "Cant connect NATS queue, retry in 10 sec..");
                    await Task.Delay(10000, cancellationToken);
                }
            } while (disconnected);

            _logService.LogInfo<MessagesRecieverHostedService>("Started!");

            var commandQueue = _queueFactory.Get();
            _subscription = _connection.SubscribeAsync(commandQueue.Key, commandQueue.Value, CreateHandler());
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logService.LogInfo<MessagesRecieverHostedService>("Stopped!");
            
            _connection.Dispose();
            _subscription.Dispose();
            _responeConnection.Dispose();
            return Task.CompletedTask;
        }

        private EventHandler<EncodedMessageEventArgs> CreateHandler()
        {
            return async (sender, args) =>
            {
                var obj = (CommandNatsAdapter) args.ReceivedObject;
                _logService.LogDebug<MessagesRecieverHostedService>($"accepted msg: {obj.CommandType} - {obj.Command}");

                var typeArgs = obj.CommandType.Split('|');
                var reply = typeArgs[1];
                var commandType = typeArgs[0];
                
                Result result;
                var cmdType = _typeFactory.Get(commandType);
                if (cmdType == null)
                {
                    var err = $"No handler found for {obj.CommandType}";
                    _logService.LogError<MessagesRecieverHostedService>($"No handler found for {obj.CommandType}");
                    result = Result.Fail(err);
                    SendResult(reply, result);
                }

                ITansMessage data = null;
                try
                {
                    data = _serializer.DeserializeMsg<ITansMessage>(obj.Command, cmdType);
                }
                catch (Exception ex)
                {
                    var err = $"Error when deserializing {obj.CommandType}";
                    _logService.LogError<MessagesRecieverHostedService>(ex, err);
                    result = Result.Fail(err);
                }

                if (data != null)
                {
                    try
                    {
                        result = await _messageSender.Send(data);
                    }
                    catch (Exception ex)
                    {
                        var err = $"Error from handler {ex.Message}";
                        _logService.LogError<MessagesRecieverHostedService>(ex, err);
                        result = Result.Fail(err);
                    }
                }

                SendResult(reply, result);
            };
        }

        private void SendResult(string reply, Result result)
        {
            try
            {
                _responeConnection.Publish(reply, new ResultAdapter
                {
                    IsSuccess = result.IsSuccess,
                    Data = result.IsSuccess ? string.Empty : result.Error
                });
                _responeConnection.Flush();
            }
            catch (Exception ex)
            {
                _logService.LogError<MessagesRecieverHostedService>(ex, "Error when publish result");
            }
        }
    }
}