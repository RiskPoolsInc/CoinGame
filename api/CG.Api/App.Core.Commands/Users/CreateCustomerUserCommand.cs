using App.Core.ViewModels.Security;
using App.Security.Annotation;

namespace App.Core.Commands.Users {

[Access]
public class CreateCustomerUserCommand : IRequest<UserBaseView>
{
    public DateTime CreatedOn { get; set; }
    public UserBaseView CreatedBy { get; set; }
    public string IP { get; set; }
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Email { get; set; }
    public bool IsActive { get; set; }
    public Guid[] Teams { get; set; }
    public Guid? OfficeId { get; set; }
}
}