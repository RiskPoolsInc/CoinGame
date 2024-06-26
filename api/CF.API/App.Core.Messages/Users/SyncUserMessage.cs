using MediatR;

namespace App.Core.Messages.Users {

public class SyncUserMessage : INotification
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName => $"{FirstName} {LastName}";
    public string Email { get; set; }
    public bool IsActive { get; set; }
}
}