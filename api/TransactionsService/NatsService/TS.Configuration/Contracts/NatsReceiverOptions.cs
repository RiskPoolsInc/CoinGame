namespace TS.Configuration.Contracts
{
    /// <summary>
    /// Nats options contract
    /// </summary>
    public class NatsReceiverOptions
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// QUeue
        /// </summary>
        public string Queue { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
    }
}