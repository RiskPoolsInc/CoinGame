using App.Security.Annotation;

namespace App.Core.Commands.Users
{
    [AdminAccess]
    public class UserNewPasswordCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Password { get; set; }
    }
}