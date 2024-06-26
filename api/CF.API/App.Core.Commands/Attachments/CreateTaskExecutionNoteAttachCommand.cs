using App.Core.Enums;
using App.Security.Annotation;

namespace App.Core.Commands.Attachments {

[Access]
public class CreateTaskExecutionNoteAttachCommand : AttachmentCommand
{
    public CreateTaskExecutionNoteAttachCommand(Guid taskId, AttachFileModel model) : base(taskId, model) { }

    public CreateTaskExecutionNoteAttachCommand()
    {
        
    }
    
    public override ObjectTypes ObjectType => ObjectTypes.TaskExecutionNote;
}
}