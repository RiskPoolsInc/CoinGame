using MediatR;

namespace App.Core.Messages.Users {

public class UserActivatedMessage : INotification
{
    public Guid Id { get; set; }
}
}