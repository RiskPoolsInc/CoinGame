using App.Data.Criterias.Core;
using App.Data.Criterias.Core.Helpers;
using App.Data.Entities;
using System;
using System.Linq.Expressions;

namespace App.Data.Criterias.Attachments {
    public class AttachmentFilter<TAttachment> : PagedCriteria<TAttachment> where TAttachment: Attachment {
        public Guid? ObjectId { get; set; }
        public int? TypeId { get; set; }

        public override Expression<Func<TAttachment, bool>> Build() {
            var criteria = True;
            if (ObjectId.HasValue)
                criteria = criteria.And(new AttachmentByObjectId<TAttachment>(ObjectId.Value));
            if (TypeId.HasValue)
                criteria = criteria.And(new AttachmentByTypeId<TAttachment>(TypeId.Value));

            if (String.IsNullOrWhiteSpace(Sort))
                SetSortBy(a => a.CreatedOn);

            return criteria.Build();
        }
    }
}
