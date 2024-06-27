using App.Core.ViewModels;
using App.Security.Annotation;

namespace App.Core.Commands.Images {

[Access]
public abstract class DeleteImageCommand : IRequest<ExternalLink>
{
    public DeleteImageCommand(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}
}