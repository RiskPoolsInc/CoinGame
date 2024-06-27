using System.Linq.Expressions;
using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;

namespace App.Data.Criterias.Notifications;

public class NotificationByRecipientId : ACriteria<Notification>
{
    private readonly Guid _recipientId;

    public NotificationByRecipientId(Guid recipientId)
    {
        _recipientId = recipientId;
    }

    public override Expression<Func<Notification, bool>> Build()
    {
        return a => a.Recipient.Id == _recipientId || a.Recipient.UbikiriUserId == _recipientId;
    }
}