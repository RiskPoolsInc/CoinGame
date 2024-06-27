using App.Data.Criterias.Core;
using App.Data.Entities.Notifications;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Notifications {
    public class NotificationByRecipientId : ACriteria<Notification> {
        public NotificationByRecipientId(Guid recipientId) {
            _recipientId = recipientId;
        }
        private readonly Guid _recipientId;

        public override Expression<Func<Notification, bool>> Build() {
            return a => a.RecipientId == _recipientId;
        }
    }
}
