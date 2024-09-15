using App.Core.ViewModels.External;
using App.Core.ViewModels.Transactions;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.TransactionReceivers;
using App.Data.Entities.Transactions;
using App.Data.Entities.Wallets;

using AutoMapper;

namespace App.Data.Mapping;

public class EntityToViewProfile : Profile {
    public EntityToViewProfile() {
        #region Transaction

        CreateMap<ATransaction, TransactionView>();
        CreateMap<TransactionReceiver, TransactionReceiverView>();

        #endregion

        #region Wallets

        CreateMap<Wallet, WalletView>();

        #endregion
    }
}