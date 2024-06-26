using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Users;
using MediatR;

namespace App.Interfaces.Handlers.RequestHandlers;

public interface IUserProfileHandler : IRequestHandler<GetUserProfileByUbikiriIdRequest, UserProfileView> {
}