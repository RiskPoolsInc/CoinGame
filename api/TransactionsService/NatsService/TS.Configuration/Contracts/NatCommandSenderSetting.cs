namespace TS.Configuration.Contracts
{
    /// <summary>
    /// Nats cmd send settings 
    /// </summary>
    public class NatCommandSenderSetting
    {
        /// <summary>
        /// Msges key
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// Msges queue name
        /// </summary>
        public string Queue { get; set; }
        /// <summary>
        /// Subject
        /// </summary>
        public string Subject { get; set; }
    }
}