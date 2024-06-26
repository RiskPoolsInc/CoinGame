using App.Core.Commands.Attachments;
using App.Core.Requests.Handlers.Attachments;
using App.Core.ViewModels.Attachments;
using App.Data.Entities.Attachments;
using App.Interfaces.Data.Storage;
using App.Interfaces.Repositories.Attachments;
using App.Interfaces.Security;

namespace App.Core.Commands.Handlers.Attachments {

public class CreateAuditLogAttachmentHandler : AttachmentHandler<AuditLogAttachment>, IRequestHandler<CreateAuditLogAttachCommand, AttachmentView>
{
    public CreateAuditLogAttachmentHandler(GetAttachmentHandler getAttachmentHandler,
        IAttachmentStorage attachmentStorage,
        IAuditLogAttachmentRepository repository,
        IContextProvider contextProvider,
        IMapper mapper
    ) : base(getAttachmentHandler, attachmentStorage, repository, contextProvider, mapper)
    {
    }

    public Task<AttachmentView> Handle(CreateAuditLogAttachCommand request, CancellationToken cancellationToken)
    {
        return CreateAttachmentAsync(request, cancellationToken);
    }
}
}