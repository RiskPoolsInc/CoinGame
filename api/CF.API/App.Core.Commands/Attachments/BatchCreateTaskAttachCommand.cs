using App.Core.ViewModels;
using App.Core.ViewModels.Attachments;
using Microsoft.AspNetCore.Http;

namespace App.Core.Commands.Attachments; 

[Access]
public class BatchCreateTaskAttachCommand : IRequest<AttachmentView[]> {
    public BatchCreateTaskAttachCommand() {
    }

    public BatchCreateTaskAttachCommand(IFormFile file, IEnumerable<BaseView> tasks, Guid customerId) {
        Commands = tasks.Select(task => new CreateTaskAttachCommand(task.Id, new AttachFileModel { File = file }, customerId)).ToArray();
    }

    public BatchCreateTaskAttachCommand(IFormFile file, Guid[] taskIds, Guid customerId) {
        Commands = taskIds.Select(id => new CreateTaskAttachCommand(id, new AttachFileModel { File = file }, customerId)).ToArray();
    }

    public CreateTaskAttachCommand[] Commands { get; set; }
}