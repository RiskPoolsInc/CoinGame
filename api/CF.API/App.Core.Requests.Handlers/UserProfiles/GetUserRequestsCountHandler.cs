using App.Core.Requests.UsersProfiles;
using App.Interfaces.Repositories;
using App.Interfaces.Security;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetUserRequestsCountHandler : IRequestHandler<GetUserRequestsCountRequest, int> {
    private readonly ICurrentUser _currentUser;
    private readonly ITaskTakeRequestRepository _takeRequestRepository;

    public GetUserRequestsCountHandler(IContextProvider           contextProvider,
                                       ITaskTakeRequestRepository takeRequestRepository) {
        _takeRequestRepository = takeRequestRepository;

        _currentUser = contextProvider.Context;
    }

    public async Task<int> Handle(GetUserRequestsCountRequest request, CancellationToken cancellationToken) {
        var count = await _takeRequestRepository.Where(a => a.UserProfileId == _currentUser.UserProfileId)
                                                .CountAsync(cancellationToken);
        return count;
        ;
    }
}