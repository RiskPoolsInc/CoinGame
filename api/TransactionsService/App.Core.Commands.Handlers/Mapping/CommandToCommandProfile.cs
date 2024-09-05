using App.Common.Helpers;
using App.Core.Commands.Wallets;
using App.Core.Enums;

namespace App.Core.Commands.Handlers.Mapping;

public class CommandToCommandProfile : Profile {
    public CommandToCommandProfile() {
        CreateMap<ImportWalletCommand, CreateWalletCommand>()
           .ForMember(a => a.TypeId, opt => opt.SetValue(nameof(CreateWalletCommand.TypeId), (int)WalletTypes.Imported));
    }
}