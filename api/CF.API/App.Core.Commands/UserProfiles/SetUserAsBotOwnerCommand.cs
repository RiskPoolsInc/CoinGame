namespace App.Core.Commands.UserProfiles;

public class SetUserAsBotOwnerCommand : IRequest<bool> {
    public SetUserAsBotOwnerCommand(Guid id) {
        Id = id;
    }

    public SetUserAsBotOwnerCommand() {
    }

    public Guid Id { get; set; }
}