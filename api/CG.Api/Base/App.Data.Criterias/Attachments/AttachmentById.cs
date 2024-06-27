using App.Data.Criterias.Core;
using App.Data.Entities;
using System;
using System.Linq.Expressions;
using App.Data.Entities.Attachments;

namespace App.Data.Criterias.Attachments {
    public class AttachmentById<TEntity> : ACriteria<TEntity> where TEntity : Attachment {
        public AttachmentById(Guid Id) {
            _Id = Id;
        }
        private readonly Guid _Id;

        public override Expression<Func<TEntity, bool>> Build() {
            return a => a.Id == _Id;
        }
    }

    public class AttachmentById : AttachmentById<Attachment> {
        public AttachmentById(Guid Id) : base(Id) {
        }
    }
}
