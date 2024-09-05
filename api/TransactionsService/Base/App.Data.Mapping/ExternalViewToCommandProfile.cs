using App.Core.ViewModels.External;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class ExternalViewToCommandProfile : Profile {
    public ExternalViewToCommandProfile() {
        CreateMap<GeneratedWalletView, Wallet>()
           .ForMember(a => a.Address, opt => opt.MapFrom(s => s.Address))
           .ForMember(a => a.PrivateKey, opt => opt.MapFrom(s => s.PrivateKey))
           .ForAllOtherMembers(s => s.Ignore());
    }
}