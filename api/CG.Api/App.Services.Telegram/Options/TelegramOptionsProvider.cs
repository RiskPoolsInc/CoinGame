using Microsoft.Extensions.Configuration;

namespace App.Services.Telegram.Options; 

public class TelegramOptionsProvider {
    public const string SETTING_NAME = nameof(TelegramOptions);

    public static TelegramOptions Get(IConfiguration configuration) {
        var section = configuration.GetSection(SETTING_NAME);
        var options = section.Get<TelegramOptions>();
        return options;
    }
}