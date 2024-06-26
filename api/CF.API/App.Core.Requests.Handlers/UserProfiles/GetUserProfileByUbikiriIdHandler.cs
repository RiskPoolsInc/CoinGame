using App.Common.Helpers;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Users;
using App.Data.Criterias.Profiles;
using App.Data.Entities.UserProfiles;
using App.Interfaces.Repositories;
using App.Interfaces.Security;

using MediatR;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetUserProfileByUbikiriIdHandler : IRequestHandler<GetUserProfileByUbikiriIdRequest, UserProfileView>,
                                                IRequestHandler<GetCurrentUserProfileRequest, UserProfileView> {
    private readonly ICurrentUser _currentUser;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetUserProfileByUbikiriIdHandler(IUserProfileRepository userProfileRepository, IContextProvider contextProvider) {
        _userProfileRepository = userProfileRepository;
        _currentUser = contextProvider.Context;
    }

    public async Task<UserProfileView> Handle(GetCurrentUserProfileRequest request, CancellationToken cancellationToken) {
        var result = await Handle(new GetUserProfileByUbikiriIdRequest(_currentUser.UserId), cancellationToken);
        return result;
    }

    public async Task<UserProfileView> Handle(GetUserProfileByUbikiriIdRequest request, CancellationToken cancellationToken) {
        var result = await _userProfileRepository.Where(new UserProfileByUbikiriUserId(request.UbikiriUserId))
                                                 .SingleAsync<UserProfile, UserProfileView>(cancellationToken);
        return result;
    }
}