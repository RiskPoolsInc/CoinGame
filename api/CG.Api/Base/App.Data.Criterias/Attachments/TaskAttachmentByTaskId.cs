using App.Data.Entities.Attachments;
using System;

namespace App.Data.Criterias.Attachments {
    public class TaskAttachmentByTaskId : AttachmentByObjectId<TaskAttachment> {
        public TaskAttachmentByTaskId(Guid taskId) : base(taskId) { }
    }
}
