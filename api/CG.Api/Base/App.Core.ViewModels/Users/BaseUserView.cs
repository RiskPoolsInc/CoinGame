namespace App.Core.ViewModels.Users;

public class BaseUserView : BaseView {
    public string Email { get; set; }
    public string? TwitterId { get; set; }
    public bool TwitterConfirmed { get; set; }
    public string? TelegramId { get; set; }
    public bool TelegramConfirmed { get; set; }
    public string? LinkedWallet { get; set; }
}