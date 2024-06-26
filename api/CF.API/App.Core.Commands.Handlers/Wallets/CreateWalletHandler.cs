using App.Common.Helpers;
using App.Core.Commands.ExternalSystems.Wallets;
using App.Core.Commands.Wallets;
using App.Core.Requests.Tasks;
using App.Core.Requests.UsersProfiles;
using App.Core.ViewModels.CustomerCompanies;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Companies;
using App.Data.Entities.Wallets;
using App.Interfaces.Core;
using App.Interfaces.Repositories;
using App.Interfaces.Repositories.Wallets;

namespace App.Core.Commands.Handlers.Wallets;

public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, WalletView> {
    private readonly ICustomerCompanyRepository _companyRepository;
    private readonly IDispatcher _dispatcher;
    private readonly IWalletRepository _walletRepository;

    public CreateWalletHandler(IWalletRepository          walletRepository, IDispatcher dispatcher) {
        _walletRepository = walletRepository;
        _dispatcher = dispatcher;
    }

    public async Task<WalletView> Handle(CreateWalletCommand request, CancellationToken cancellationToken) {
        var generateWalletCommand = new GenerateWalletHandler();
        var generatedWallet = await _dispatcher.Send(generateWalletCommand, cancellationToken);
        var walletAddress = customerCompanyView.Wallet.Address;
        
        var wallet = new Wallet {
            Hash = generatedWallet.Hash,
            TypeId = (int)WalletTypes,
            PrivateKey = null,
            IsBlocked = false,
            RefundId = default,
            Refund = default
        };
        _walletRepository.Add(wallet);
        await _walletRepository.SaveAsync(cancellationToken);

        var model = await _walletRepository.Get(wallet.Id).SingleAsync<Wallet, WalletView>(cancellationToken);
        return model;
    }
}