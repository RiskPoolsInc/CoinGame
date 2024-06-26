namespace App.Core.Commands.Telegram;

public class TelegramMessageCommand {
    public string Message { get; set; }
    public string Receiver { get; set; }
    public Guid? UserProfileId { get; set; }
    public TgmMessageEntityCommand[]? MessageEntities { get; set; }
}