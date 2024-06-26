using App.Common.Helpers;
using App.Core.Enums;
using App.Core.Requests.Settings;
using App.Core.ViewModels.Settings;
using App.Data.Entities.Settings;
using App.Interfaces.Repositories.Settings;
using App.Interfaces.Security;
using MediatR;

namespace App.Core.Requests.Handlers.Settings;

public class GetSettingsHandler : IRequestHandler<GetSettings, SettingPropertyView[]>
{
    private readonly ISettingPropertyRepository _repository;
    private readonly ICurrentUser _user;

    public GetSettingsHandler(ISettingPropertyRepository repository, IContextProvider contextProvider)
    {
        _repository = repository;
        this._user = contextProvider.Context;
    }

    public async Task<SettingPropertyView[]> Handle(GetSettings request, CancellationToken cancellationToken)
    {
        var query = _repository.GetAll();
        if (_user.IsExecutor)
            query = query.Where(a => _executorAccessAllowedSettings.Contains(a.TypeId));
        else if (!_user.IsAdmin)
            query = query;//TODO ADD using _companyAccessAllowedSettings after modify admin rules

        var result = await query.ToArrayAsync<SettingProperty, SettingPropertyView>(cancellationToken);
        return result;
    }

    private readonly int[] _executorAccessAllowedSettings =
    {
        (int) SettingPropertyTypes.RequiredTelegramConfirmation
    };

    private readonly int[] _companyAccessAllowedSettings =
    {
        (int) SettingPropertyTypes.RequiredTelegramConfirmation,
    };
}