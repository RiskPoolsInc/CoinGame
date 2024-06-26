namespace App.Core.Commands.Telegram;

public class PublishTaskTelegramCommand : IRequest<bool> {
    public Guid TaskId { get; set; }
    public string Message { get; set; }
    public TgmMessageEntityCommand[]? Entites { get; set; }
}