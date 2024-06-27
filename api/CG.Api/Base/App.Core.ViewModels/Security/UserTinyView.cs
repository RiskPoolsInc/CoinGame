namespace App.Core.ViewModels.Security;

public class UserTinyView : UserView
{
    public Guid? UbikiriUserId { get; set; }
    public bool IsDeleted { get; set; }
    public string? TwitterId { get; set; }
    public string? TelegramId { get; set; }
}