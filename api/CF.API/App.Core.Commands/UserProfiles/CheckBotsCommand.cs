namespace App.Core.Commands.UserProfiles;

[AdminAccess]
public class CheckBotsCommand : IRequest<bool> {
    public Guid[]? UserIds { get; set; }
}