using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.Options;

using SilentNotary.Common;
using SilentNotary.Common.Query.Criterion.Abstract;
using SilentNotary.Cqrs.Nats.Abstract;

using TS.Configuration.Contracts;

namespace TS.Configuration
{
    public class AppNatsCommandQueueFactory : INatsReceiverCommandQueueFactory
    {
        private readonly NatsReceiverOptions _options;

        public AppNatsCommandQueueFactory(IOptions<NatsReceiverOptions> options)
        {
            _options = options.Value;
        }

        public KeyValuePair<string, string> Get() =>
            commandQueue ?? (commandQueue = new KeyValuePair<string, string>(
                _options.Subject,
                _options.Queue)
            ).Value;

        private KeyValuePair<string, string>? commandQueue;
    }

    public class SBNNatsQueryQueueFactory : INatsReceiverQueryQueueFactory
    {
        private readonly NatsReceiverOptions _options;

        public SBNNatsQueryQueueFactory(IOptions<NatsReceiverOptions> options)
        {
            _options = options.Value;
        }

        public string Get() => _options.Subject;
    }

    public class SBNNatsSenderQueueFactory : INatsSenderQueueFactory
    {
        private readonly NatsSenderOptions _options;

        private readonly ConcurrentDictionary<string, KeyValuePair<string, string>> _commandsCache =
            new ConcurrentDictionary<string, KeyValuePair<string, string>>();

        private readonly ConcurrentDictionary<string, string> _queryCache = new ConcurrentDictionary<string, string>();

        public SBNNatsSenderQueueFactory(IOptions<NatsSenderOptions> options)
        {
            _options = options.Value;
        }

        public KeyValuePair<string, string> GetCommandQueue(IMessage message)
        {
            var key = ((ICmdBase) message).Key;
            if (_commandsCache.TryGetValue(key, out KeyValuePair<string, string> val))
                return val;

            var setting = _options.CommandSenderSettings.FirstOrDefault(s => s.Key == key) ??
                          _options.CommandSenderSettings.First(s => s.Key == DefaultQueueCmd.DefaultKey);

            val = new KeyValuePair<string, string>(setting.Queue, setting.Subject);
            return _commandsCache.AddOrUpdate(key, val, (newkey, existingVal) => existingVal);
        }

        public string GetQueryQueue(ICriterion message, object result)
        {
            var key = ((IQueryCriterionBase) message).Key;
            if (_queryCache.TryGetValue(key, out string val))
                return val;

            var setting = _options.QuerySenderSettings.FirstOrDefault(s => s.CriteriaKey == key) ??
                          _options.QuerySenderSettings.First(s => s.CriteriaKey == QueryCriterionBase.DefaultKey);

            val = setting.Subject;
            return _queryCache.AddOrUpdate(key, val, (newkey, existingVal) => existingVal);
        }
    }
}