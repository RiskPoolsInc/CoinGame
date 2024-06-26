using App.Core.Enums;

namespace App.Core.Commands.Attachments;

public class CreateTaskAttachCommand : AttachmentCommand {
    public CreateTaskAttachCommand() {
    }
    public  CreateTaskAttachCommand(Guid taskId, AttachFileModel model) : base(taskId, model) { }

    public CreateTaskAttachCommand(Guid taskId, AttachFileModel model, Guid customerId, Guid? attachId = null)
        : base(taskId, model, customerId, attachId) {
    }

    public override ObjectTypes ObjectType => ObjectTypes.Task;
}