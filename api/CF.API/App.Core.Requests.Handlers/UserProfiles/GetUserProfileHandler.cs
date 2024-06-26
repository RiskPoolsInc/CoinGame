using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Users;
using App.Data.Entities.UserProfiles;
using App.Interfaces.Repositories;

using MediatR;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetUserProfileHandler : BaseGetEntityHandler<GetUserProfileRequest, UserProfileView, UserProfile, Guid>,
                                     IRequestHandler<GetUserProfileRequest, UserProfileView> {
    public GetUserProfileHandler(IUserProfileRepository userProfileRepository) : base(userProfileRepository) {
    }
}