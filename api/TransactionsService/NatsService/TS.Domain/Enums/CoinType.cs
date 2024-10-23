using System.Runtime.Serialization;

namespace TS.Domain.Enums
{
    [DataContract]
    public enum CoinType
    {
        [EnumMember]
        Cil,

        [EnumMember]
        Eth
    }
}