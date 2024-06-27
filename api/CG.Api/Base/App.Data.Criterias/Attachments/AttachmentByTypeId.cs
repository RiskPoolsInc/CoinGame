using App.Data.Criterias.Core;
using App.Data.Entities;
using System;
using System.Linq.Expressions;
using App.Data.Entities.Attachments;

namespace App.Data.Criterias.Attachments {
    public class AttachmentByTypeId<TAttachment> : ACriteria<TAttachment> where TAttachment: Attachment {
        public AttachmentByTypeId(int typeId) {
            _typeId = typeId;
        }
        private readonly int _typeId;

        public override Expression<Func<TAttachment, bool>> Build() {
            return a => a.ObjectTypeId == _typeId;
        }
    }
}
