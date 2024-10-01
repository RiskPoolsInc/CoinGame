using System.Runtime.Serialization;

namespace TS.Domain.Enums
{
    [DataContract]
    public enum TransactionFeeType
    {
        [EnumMember]
        Slow,

        [EnumMember]
        Average,

        [EnumMember]
        Fast
    }
}
