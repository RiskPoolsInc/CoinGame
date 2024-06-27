using App.Security.Annotation;

namespace App.Core.Commands.Images {

[Access]
public class UpdateImageLocationCommand : IRequest<bool>
{
}
}