using App.Common.Helpers;
using App.Core.Requests.Settings;
using App.Core.ViewModels.Settings;
using App.Data.Entities.Settings;
using App.Interfaces.Repositories.Settings;

using MediatR;

namespace App.Core.Requests.Handlers.Settings;

public class GetSettingPropertyHandler : IRequestHandler<GetSettingProperty, SettingPropertyView> {
    private readonly ISettingPropertyRepository _repository;

    public GetSettingPropertyHandler(ISettingPropertyRepository repository) {
        _repository = repository;
    }

    public Task<SettingPropertyView> Handle(GetSettingProperty request, CancellationToken cancellationToken) {
        return _repository.Where(a => a.TypeId == request.TypeId)
                          .SingleAsync<SettingProperty, SettingPropertyView>(cancellationToken);
    }
}