using App.Core.ViewModels.Dictionaries;

namespace App.Core.ViewModels.ChangeReasons;

public class ChangeReasonView : BaseView
{
    public ChangeReasonTypeView Type { get; set; }
    public string Text { get; set; } 
}