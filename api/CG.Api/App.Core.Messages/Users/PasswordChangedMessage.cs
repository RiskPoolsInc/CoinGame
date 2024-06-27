using MediatR;

namespace App.Core.Messages.Users {

public class PasswordChangedMessage : INotification
{
    public Guid UserId { get; set; }
}
}