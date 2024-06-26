using App.Core.Enums;
using App.Core.ViewModels.Attachments;

using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Attachments {
    public abstract class AttachmentCommand : IRequest<AttachmentView> {
        public Guid CustomerId { get; set; }
        public Guid? Id { get; }

        public AttachmentCommand(Guid objectId, AttachFileModel model, Guid customerId, Guid? id = null) : this(objectId, model.File) {
            CustomerId = customerId;
            Id = id;
        }

        public AttachmentCommand() {
        }

        public AttachmentCommand(Guid objectId, AttachFileModel model) : this(objectId, model.File) {
        }

        public AttachmentCommand(Guid objectId, IFormFile model) : this() {
            ObjectId = objectId;
            File = model;
        }

        public int TypeId { get; set; } = (int)AttachmentTypes.File;
        public string? Description { get; set; }

        public abstract ObjectTypes ObjectType { get; }
        public Guid? ObjectId { get; set; }
        public IFormFile? File { get; }
    }
}