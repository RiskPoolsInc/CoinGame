namespace App.Core.Commands.Telegram;

public class CheckTelegramMessageCommand : TelegramMessageCommand, IRequest<object> {
    public string? Sender { get; set; }
    public string? Message { get; set; }
    public string Receiver { get; set; }
}