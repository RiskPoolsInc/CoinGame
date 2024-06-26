using App.Core.ViewModels.ChangeReasons;
using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.Log;

public abstract class BasePropertyHistoryView : BaseView
{
    public ObjectTypeView? ObjectType { get; set; }
    public Guid ObjectId { get; set; }
    public PropertyTypeView? PropertyType { get; set; }
    public ObjectValueTypeView? ObjectValueType { get; set; }
    public bool Nullable { get; set; }
    public ChangeReasonView? ChangeReason { get; set; }
}