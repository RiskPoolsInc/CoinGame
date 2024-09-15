using App.Core.Commands.Wallets;
using App.Core.ViewModels.External;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class ExternalViewProfile : Profile {
    public ExternalViewProfile() {
        CreateMap<GeneratedWalletView, Wallet>()
           .ForMember(a => a.Address, opt => opt.MapFrom(s => s.Address))
           .ForMember(a => a.PrivateKey, opt => opt.MapFrom(s => s.PrivateKey))
           .ForAllOtherMembers(s => s.Ignore());

        CreateMap<GeneratedWalletView, CreateWalletCommand>()
           .ForMember(a => a.Address, opt => opt.MapFrom(s => s.Address))
           .ForMember(a => a.PrivateKey, opt => opt.MapFrom(s => s.PrivateKey))
           .ForAllOtherMembers(s => s.Ignore());

        CreateMap<GeneratedWalletView, CreateImportedWalletCommand>()
           .IncludeBase<GeneratedWalletView, CreateWalletCommand>();

        CreateMap<GeneratedWalletView, CreateGeneratedWalletCommand>()
           .IncludeBase<GeneratedWalletView, CreateWalletCommand>();
    }
}