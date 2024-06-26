using App.Common.Helpers;
using App.Core.Requests.Notifications;
using App.Core.ViewModels;
using App.Core.ViewModels.Notifications;
using App.Data.Criterias.Notifications;
using App.Data.Entities.Notifications;
using App.Interfaces.Repositories.Notifications;
using App.Interfaces.Security;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Notifications; 

public class GetNotificationsHandler : IRequestHandler<GetNotificationsRequest, PagedList<NotificationView>> {
    private readonly ICurrentUser _currentUser;
    private readonly INotificationRepository _notificationRepository;

    public GetNotificationsHandler(INotificationRepository notificationRepository, IContextProvider contextProvider) {
        _notificationRepository = notificationRepository;
        _currentUser = contextProvider.Context;
    }

    public async Task<PagedList<NotificationView>> Handle(GetNotificationsRequest request, CancellationToken cancellationToken) {
        var query = _notificationRepository.Where(new NotificationByRecipientId(_currentUser.UserProfileId).Build());
        var totalCount = await query.CountAsync(cancellationToken);

        var result = await query.OrderByDescending(a => a.CreatedOn)
                                .Skip(request.Skip.Value)
                                .Take(request.Take.Value)
                                .ToArrayAsync<Notification, NotificationView>(cancellationToken);
        return new PagedList<NotificationView>(result, totalCount);
    }
}