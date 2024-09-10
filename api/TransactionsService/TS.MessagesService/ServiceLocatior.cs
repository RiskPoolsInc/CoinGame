using SilentNotary.Common;

namespace TS.MessagesService
{
    public class ServiceLocatior : IDiScope
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceLocatior(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public TSvc Resolve<TSvc>()
        {
            return _serviceProvider.GetRequiredService<TSvc>();
        }
        
        public object Resolve(Type type)
        {
            return _serviceProvider.GetService(type);
        }
    }
}