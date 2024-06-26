using App.Common.Helpers;
using App.Core.Commands.Settings;
using App.Core.Enums;
using App.Core.ViewModels.Settings;
using App.Data.Entities.Settings;
using App.Interfaces.Handlers.CommandHandlers;
using App.Interfaces.Repositories.Settings;

namespace App.Core.Commands.Handlers.Settings;

public class UpdateSettingPropertyHandler : IUpdateTransactionTaxHandler,
                                            IUpdateDefaultReferralAwardHandler,
                                            IUpdateTaxWalletHandler,
                                            IRequestHandler<UpdateSettingProperty, SettingPropertyView> {
    private readonly IMapper _mapper;
    protected readonly ISettingPropertyRepository SettingPropertyRepository;

    public UpdateSettingPropertyHandler(ISettingPropertyRepository settingPropertyRepository, IMapper mapper) {
        SettingPropertyRepository = settingPropertyRepository;
        _mapper = mapper;
    }

    public async Task<SettingPropertyView> Handle(UpdateSettingProperty request,
                                                  CancellationToken     cancellationToken) {
        var entity = await SettingPropertyRepository.FindAsync(request.Id, cancellationToken);

        var valueType = (SettingPropertyValueTypes)entity.ValueTypeId;

        switch (valueType) {
            case SettingPropertyValueTypes.Int:
                entity.ValueNumber = request.ValueNumber;
                entity.ValueNumberDouble = null;
                entity.ValueText = null;
                break;
            case SettingPropertyValueTypes.Double:
                entity.ValueNumber = null;
                entity.ValueNumberDouble = request.ValueNumberDouble;
                entity.ValueText = null;
                break;
            case SettingPropertyValueTypes.Text:
                entity.ValueNumber = null;
                entity.ValueNumberDouble = null;
                entity.ValueText = request.ValueText;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        await SettingPropertyRepository.SaveAsync(cancellationToken);

        return await SettingPropertyRepository.Get(request.Id)
                                              .SingleAsync<SettingProperty, SettingPropertyView>(cancellationToken);
    }

    public Task<SettingPropertyView> Handle(UpdateDefaultReferralAwardCommand request,
                                            CancellationToken                 cancellationToken) {
        return Handle(_mapper.Map<UpdateSettingProperty>(request), cancellationToken);
    }

    public Task<SettingPropertyView> Handle(UpdateTaxWalletCommand request, CancellationToken cancellationToken) {
        return Handle(_mapper.Map<UpdateSettingProperty>(request), cancellationToken);
    }

    public Task<SettingPropertyView> Handle(UpdateTransactionTaxCommand request, CancellationToken cancellationToken) {
        return Handle(_mapper.Map<UpdateSettingProperty>(request), cancellationToken);
    }
}