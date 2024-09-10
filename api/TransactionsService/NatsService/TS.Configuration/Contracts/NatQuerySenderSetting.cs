namespace TS.Configuration.Contracts
{
    /// <summary>
    /// Nats query sender settings
    /// </summary>
    public class NatQuerySenderSetting
    {
        public string CriteriaKey { get; set; }
        public string Subject { get; set; }
        public int Timeout { get; set; }
    }
}