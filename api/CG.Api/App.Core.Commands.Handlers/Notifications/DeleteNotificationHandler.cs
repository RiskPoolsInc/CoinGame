using App.Core.Commands.Notifications;
using App.Core.Exceptions;
using App.Interfaces.Repositories.Notifications;

namespace App.Core.Commands.Handlers.Notifications; 

public class DeleteNotificationHandler : IRequestHandler<DeleteNotificationCommand, bool> {
    private readonly INotificationRepository _notificationRepository;

    public DeleteNotificationHandler(INotificationRepository notificationRepository) {
        _notificationRepository = notificationRepository;
    }

    public async Task<bool> Handle(DeleteNotificationCommand request, CancellationToken cancellationToken) {
        _notificationRepository.Delete(request.Id);
        var affectedRows = await _notificationRepository.SaveAsync(cancellationToken);
        return affectedRows > 0 ? true : throw new EntityNotFoundException("Notification", request.Id);
    }
}