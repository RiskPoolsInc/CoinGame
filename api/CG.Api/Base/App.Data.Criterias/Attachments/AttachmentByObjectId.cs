using App.Data.Criterias.Core;
using App.Data.Entities;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Attachments {
    public class AttachmentByObjectId<TEntity> : ACriteria<TEntity> where TEntity : Attachment {
        public AttachmentByObjectId(Guid objectId) {
            _objectId = objectId;
        }
        private readonly Guid _objectId;

        public override Expression<Func<TEntity, bool>> Build() {
            return a => a.ObjectId == _objectId;
        }
    }

    public class AttachmentByObjectId : AttachmentByObjectId<Attachment> {
        public AttachmentByObjectId(Guid objectId) : base(objectId) {
        }
    }
}
