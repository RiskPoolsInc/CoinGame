namespace App.Core.ViewModels.Security;

public class UserView : UserBaseView
{
    public string Email { get; set; }
    public string TwitterId { get; set; }
    public string TelegramId { get; set; }    
}