using App.Core.Commands.Wallets;

namespace App.Core.Commands.Handlers.Mapping;

public class CommandToCommandProfile : Profile {
    public CommandToCommandProfile() {
        CreateMap<ImportWalletCommand, CreateWalletCommand>();
           // .ForMember(a => a.TypeId, opt => opt.Ignore());

        CreateMap<ImportWalletCommand, CreateImportedWalletCommand>()
           .IncludeBase<ImportWalletCommand, CreateWalletCommand>();
    }
}