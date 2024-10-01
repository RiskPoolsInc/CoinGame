using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Hangfire;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SilentNotary.Common;

namespace TS.Infrastructure.Slave
{
    public abstract class MessageSendingHangfireHostedRecurringServiceBase<TCmd> : HostedServiceBase
        where TCmd : IMessage
    {
        private readonly IServiceProvider _serviceProvider;

        protected MessageSendingHangfireHostedRecurringServiceBase(ILoggerFactory logger,
            IServiceProvider serviceProvider,
            bool enabled)
            : base(logger, enabled)
        {
            _serviceProvider = serviceProvider;
        }

        protected override Task ExecuteAsync(CancellationToken cancellationToken)
        {
            try
            {
                RecurringJob.AddOrUpdate(ToString(), () => Run(JobCancellationToken.Null), GetCronExpression()); 
            }
            catch (Exception ex)
            {
                Logger.LogError(ex.ToString());
            }

            return Task.CompletedTask;
        }

        public async Task Run(IJobCancellationToken token)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var messageSender = scope.ServiceProvider.GetRequiredService<IMessageSender>();
                await CmdFactoryMethod()
                    .OnSuccess(async command => await messageSender.SendAsync(command), true)
                    .OnFailure(error => Logger.LogError(error));
            }
        }

        protected abstract Result<TCmd> CmdFactoryMethod();

        protected virtual string GetCronExpression()
        {
            return Cron.Minutely();
        }
    }
}