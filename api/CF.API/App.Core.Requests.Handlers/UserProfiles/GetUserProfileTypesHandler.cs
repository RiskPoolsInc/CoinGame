using App.Common.Helpers;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.Dictionaries;
using App.Data.Entities.Dictionaries;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Dictionaries;
using MediatR;

namespace App.Core.Requests.Handlers.UserProfiles;

public class GetUserProfileTypesHandler : IRequestHandler<GetUserProfileTypesRequest, UserTypeView[]> {
    private readonly IUserTypeRepository _repository;

    public GetUserProfileTypesHandler(IUserTypeRepository repository) {
        _repository = repository;
    }

    public Task<UserTypeView[]> Handle(GetUserProfileTypesRequest request, CancellationToken cancellationToken) {
        return _repository.GetAll().ToArrayAsync<UserType, UserTypeView>(cancellationToken);
    }
}