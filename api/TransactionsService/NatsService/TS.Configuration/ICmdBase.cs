using SilentNotary.Common;

namespace TS.Configuration
{
    public interface ICmdBase : ICommandData
    {
        string Key { get; }
    }

    public abstract class DefaultQueueCmd : ICmdBase
    {
        public const string DefaultKey = "DefaultQueueCmd";
        public string Key => DefaultKey;
    }
}