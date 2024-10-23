using App.Core.Commands.Wallets;
using App.Data.Entities;
using App.Data.Entities.Wallets;

namespace App.Core.Commands.Handlers.Mapping
{
    public class CommandToEntityProfile : Profile
    {
        public CommandToEntityProfile()
        {
            CreateMap<DictionaryItem, DictionaryName>();
 
            #region Wallets

            CreateMap<CreateWalletCommand, Wallet>();

            #endregion
        }
    }
}