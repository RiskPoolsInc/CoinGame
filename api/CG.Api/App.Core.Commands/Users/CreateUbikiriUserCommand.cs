using App.Core.ViewModels.Security;

namespace App.Core.Commands.Users
{
    public class CreateUbikiriUserCommand : IRequest<UserTinyView>
    {
        public Guid UbikiriUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}