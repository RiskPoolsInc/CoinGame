using App.Interfaces.Core;

using SilentNotary.Common;
using SilentNotary.Cqrs.Nats.Abstract;

using TS.Configuration;
using TS.Infrastructure.Slave;

namespace TS.CommandHandlersHost.HostedServices {
    public class MessageRecieverHostedService : MessagesRecieverHostedService {
        public MessageRecieverHostedService(INatsSerializer serializer, IDispatcher messageSender,
                                            ILogService logService, INatsConnectionFactory connectionFactory, ITypeFactory typeFactory,
                                            INatsReceiverCommandQueueFactory queueFactory)
            : base(serializer, messageSender, connectionFactory, typeFactory,
                queueFactory, logService) {
        }
    }
}