using App.Core.ViewModels.External;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class ExternalViewToCommandProfile : Profile {
    public ExternalViewToCommandProfile() {
        CreateMap<GeneratedWalletView, Wallet>()
           .ForMember(a => a.Hash, opt => opt.MapFrom(s => s.Address))
           .ForMember(a => a.Id, opt => opt.MapFrom(s => s.Id))
           .ForMember(a => a.ImportedWalletId, opt => opt.MapFrom(s => s.Id))
           .ForAllOtherMembers(s => s.Ignore());
    }
}