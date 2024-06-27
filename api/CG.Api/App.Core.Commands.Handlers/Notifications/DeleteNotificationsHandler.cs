using App.Core.Commands.Notifications;
using App.Data.Criterias.Notifications;
using App.Interfaces.Repositories.Notifications;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.Notifications; 

public class DeleteNotificationsHandler : IRequestHandler<DeleteNotificationsCommand, bool> {
    private readonly ICurrentUser _currentUser;
    private readonly INotificationRepository _notificationRepository;

    public DeleteNotificationsHandler(INotificationRepository notificationRepository, IContextProvider contextProvider) {
        _notificationRepository = notificationRepository;
        _currentUser = contextProvider.Context;
    }

    public async Task<bool> Handle(DeleteNotificationsCommand request, CancellationToken cancellationToken) {
        await _notificationRepository.RemoveWhereAsync(new NotificationByRecipientId(_currentUser.UserProfileId).Build(),
                                                       cancellationToken);
        return true;
    }
}