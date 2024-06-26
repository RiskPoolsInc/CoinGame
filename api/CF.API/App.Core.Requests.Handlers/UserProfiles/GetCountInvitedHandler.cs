using App.Common.Helpers;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Users;
using App.Data.Entities.UserProfiles;
using App.Interfaces.Repositories;
using App.Interfaces.Security;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetCountInvitedHandler : IRequestHandler<GetCountInvitedRequest, long> {
    private readonly ICurrentUser _currentUser;
    private readonly IUserProfileRepository _profileRepository;
    private readonly IReferralPairRepository _referralPairRepository;

    public GetCountInvitedHandler(IContextProvider        contextProvider,
                                  IUserProfileRepository  profileRepository,
                                  IReferralPairRepository referralPairRepository) {
        _profileRepository = profileRepository;
        _referralPairRepository = referralPairRepository;
        _currentUser = contextProvider.Context;
    }

    public async Task<long> Handle(GetCountInvitedRequest request, CancellationToken cancellationToken) {
        var user = await _profileRepository.Where(a => a.UbikiriUserId == _currentUser.UserId)
                                           .SingleAsync<UserProfile, UserProfileView>(cancellationToken);

        var invitedUsersCount = await _referralPairRepository.Where(a => a.InvitedByUserId == user.Id)
                                                             .CountAsync(cancellationToken);
        return invitedUsersCount;
        ;
    }
}