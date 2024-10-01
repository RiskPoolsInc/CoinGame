using App.Interfaces.Services;

namespace App.Services.Telegram.Options; 

public class TelegramGroupOptions : ITelegramGroupOptions {
    /// <summary>
    ///     Group name. User could be joined to him
    /// </summary>
    public string Id { get; set; }
}