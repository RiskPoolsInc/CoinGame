using App.Data.Entities.Core;
using App.Data.Entities.Dictionaries;

namespace App.Data.Entities.AutoPaymentServiceLogs;

public class AutoPaymentServiceLog : BaseEntity {
    public int TypeId { get; set; }
    public virtual AutoPaymentServiceLogType Type { get; set; }
}