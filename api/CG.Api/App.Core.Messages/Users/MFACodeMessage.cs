using MediatR;

namespace App.Core.Messages.Users {

public class MFACodeMessage : INotification
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string IP { get; set; }
    public string Agent { get; set; }
}
}