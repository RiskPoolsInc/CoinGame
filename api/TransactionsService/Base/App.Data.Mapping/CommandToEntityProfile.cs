using App.Core.Commands.Wallets;
using App.Data.Entities;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class CommandToEntityProfile : Profile {
    public CommandToEntityProfile() {
        CreateMap<DictionaryItem, DictionaryName>();

        #region Wallets

        CreateMap<CreateWalletCommand, Wallet>();

        #endregion
    }
}