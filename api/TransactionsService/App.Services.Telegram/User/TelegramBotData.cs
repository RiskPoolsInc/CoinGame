namespace App.Services.Telegram.User; 

/// <summary>
///     Data of your bot
/// </summary>
public class TelegramBotData {
    public TelegramBotData(string clientSecret) {
        ClientSecret = clientSecret;
    }

    public string ClientSecret { get; }
}