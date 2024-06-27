using App.Core.Messages.Audits;
using App.Data.Entities.Notifications;
using App.Interfaces.Repositories.Notifications;

using MediatR;

namespace App.Core.Messages.Handlers; 

public abstract class NotificationMessageHandler<TMessage> where TMessage : AuditBase, IRequest<NotificationMessage[]> {
    private readonly INotificationRepository _notificationRepository;

    protected NotificationMessageHandler(INotificationRepository notificationRepository) {
        _notificationRepository = notificationRepository;
    }

    public async Task<NotificationMessage[]> Handle(TMessage request, CancellationToken cancellationToken) {
        var entities = await HandleAsync(request, cancellationToken);
        await _notificationRepository.AddRangeAsync(entities, cancellationToken);
        await _notificationRepository.SaveAsync(cancellationToken);

        return entities.Select(a => new NotificationMessage {
                            Id = a.Id,
                            CreatedOn = request.CreatedOn,
                            CreatedBy = request.CreatedBy,
                            ObjectId = a.ObjectId,
                            ParentId = a.ParentId,
                            RecipientId = a.RecipientId,
                            Title = a.Title,
                            TypeId = a.TypeId,
                            JsonObjectView = a.JsonObjectView
                        })
                       .ToArray();
    }

    protected abstract Task<Notification[]> HandleAsync(TMessage request, CancellationToken cancellationToken);
}