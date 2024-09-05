using App.Security.Annotation;

namespace App.Core.Commands.Notifications; 

[Access]
public class DeleteNotificationCommand : IRequest<bool> {
    public DeleteNotificationCommand(Guid id) {
        Id = id;
    }

    public Guid Id { get; }
}