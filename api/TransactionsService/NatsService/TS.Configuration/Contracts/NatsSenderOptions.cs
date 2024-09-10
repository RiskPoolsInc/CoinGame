using System.Collections.Generic;

namespace TS.Configuration.Contracts
{
    /// <summary>
    /// Nats options contract
    /// </summary>
    public class NatsSenderOptions
    {
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Nats queue user name
        /// </summary>
        public string User { get; set; }
        /// <summary>
        /// Nats queue password
        /// </summary>
        public string Password { get; set; }

        
        public List<NatCommandSenderSetting> CommandSenderSettings { get; set; }
        public List<NatQuerySenderSetting> QuerySenderSettings { get; set; }
    }
}