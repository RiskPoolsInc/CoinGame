namespace App.Core.Commands.Telegram;

public class SendTaskToTelegramCommand : IRequest<bool>
{
    public Guid TaskId { get; set; }
}