namespace App.Core.Commands.Telegram;

public class SendTelegramMessageCommand : TelegramMessageCommand, IRequest<object> {
    public SendTelegramMessageCommand() {
    }

    public SendTelegramMessageCommand(string? sender, TelegramMessageCommand messageCommand) {
        Sender = sender;
        MessageEntities = messageCommand.MessageEntities;
        Receiver = messageCommand.Receiver;
        Message = messageCommand.Message;
        UserProfileId = messageCommand.UserProfileId;
    }

    public string? Sender { get; set; }
}