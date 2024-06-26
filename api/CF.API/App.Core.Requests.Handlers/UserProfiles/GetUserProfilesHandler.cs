using App.Core.Requests.Handlers.Helpers;
using App.Core.Requests.Users;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Users;
using App.Data.Criterias.UserProfiles;
using App.Data.Entities.UserProfiles;
using App.Interfaces.Core.Requests;
using App.Interfaces.Repositories;
using AutoMapper;
using MediatR;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetUserProfilesHandler : IRequestHandler<GetUserProfilesRequest, IPagedList<UserProfileView>>

{
    private readonly IMapper _mapper;
    private readonly IUserProfileRepository _userProfileRepository;

    public GetUserProfilesHandler(IUserProfileRepository userProfileRepository, IMapper mapper)
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
    }

    public async Task<IPagedList<UserProfileView>> Handle(GetUserProfilesRequest request, CancellationToken cancellationToken)
    {
        return await GetUsers(_mapper.Map<UserProfilesFilter>(request), cancellationToken);
    }

    private async Task<IPagedList<UserProfileView>> GetUsers(UserProfilesFilter? filter, CancellationToken cancellationToken)
    {
        var result = await _userProfileRepository.Where(filter.Build())
            .ToLookupAsync<UserProfile, UserProfileView>(filter, cancellationToken);
        return result;
    }
}