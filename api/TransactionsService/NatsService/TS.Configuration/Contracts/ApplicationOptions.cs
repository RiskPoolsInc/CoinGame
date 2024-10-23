using System.Runtime.Serialization;

namespace TS.Configuration.Contracts
{
    /// <summary>
    /// Application options contract
    /// </summary>
    [DataContract]
    public class ApplicationOptions
    {
        [DataMember]
        public string GameAddress { get; set; }

        [DataMember]
        public string OraculSmartContractAddress { get; set; }

        [DataMember]
        public int GameConsiliumIdInCil { get; set; }

        [DataMember]
        public long[] BlockGameIds { get; set; }
    }
}