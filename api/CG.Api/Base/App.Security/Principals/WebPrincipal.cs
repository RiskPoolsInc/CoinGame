using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

using App.Core.Enums;
using App.Interfaces.Repositories;
using App.Interfaces.Security;
using App.Security.JwtHelpers;

namespace App.Security.Principals;

public class WebPrincipal : ICurrentUser {
    private readonly ClaimsIdentity _identity;
    private readonly IUserProfileRepository _repository;
    private readonly UserTypes _type;

    public WebPrincipal(ClaimsIdentity? identity, IUserProfileRepository repository) {
        _repository = repository;
        _identity = identity ?? throw new ArgumentNullException("identity");
        UserId = Guid.Parse(_identity.GetValue(JwtRegisteredClaimNames.Sid));

        var user = _repository.Where(a => a.UbikiriUserId == UserId).SingleOrDefault();

        if (user == null)
            return;


        UserProfileId = user.Id;
        _type = (UserTypes)user.TypeId;
    }

    public bool IsAdmin => _type == UserTypes.Admin;
    public bool IsCustomer => _type == UserTypes.Customer || IsTaskManager || _type == UserTypes.CompanyAdmin;
    public bool IsExecutor => _type == UserTypes.Executor;
    public bool IsTaskManager => _type == UserTypes.CompanyTaskManager;
    public Guid UserId { get; set; }
    public Guid UserProfileId { get; set; }
    public bool IsAnonymous => false;
}