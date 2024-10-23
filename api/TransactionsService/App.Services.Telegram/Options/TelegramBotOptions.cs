using App.Interfaces.Services;

namespace App.Services.Telegram.Options; 

public class TelegramBotOptions : ITelegramBotOptions {
    public string ClientSecret { get; set; }
}