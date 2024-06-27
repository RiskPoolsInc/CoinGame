using App.Core.Configurations.Annotaions;
using App.Interfaces.Core.Configurations;

namespace App.Services.Telegram.Options; 

[ConfigurationName("TelegramOptions")]
public class TelegramOptions : IConfig {
    /// <summary>
    ///     Use telegram in system
    /// </summary>
    public bool Enable { get; set; }

    public TelegramBotOptions? TelegramBotOptions { get; set; }
    public TelegramGroupOptions? TelegramGroupOptions { get; set; }
}