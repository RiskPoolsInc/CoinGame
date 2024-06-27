using App.Data.Entities.Attachments;
using System;

namespace App.Data.Criterias.Attachments {
    public class TaskAttachmentById : AttachmentById<TaskAttachment> {
        public TaskAttachmentById(Guid Id) : base(Id) { }
    }
}
