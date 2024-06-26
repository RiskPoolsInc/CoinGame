using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.Security;

namespace App.Core.ViewModels.ChatMessages;

public class ChatMessageView : BaseView
{
    public UserTinyView CreatedBy { get; set; }
    public ChatObjectTypeView ChatObjectType { get; set; }
    public string? Message { get; set; }
}