using App.Common.Helpers;
using App.Core.Commands.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

namespace App.Core.Commands.Handlers.Wallets;

public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, WalletView> {
    private readonly IMapper _mapper;
    private readonly IWalletRepository _walletRepository;

    public CreateWalletHandler(IMapper mapper, IWalletRepository walletRepository) {
        _mapper = mapper;
        _walletRepository = walletRepository;
    }

    public async Task<WalletView> Handle(CreateWalletCommand request, CancellationToken cancellationToken) {
        var wallet = _mapper.Map<Wallet>(request);
        wallet.PrivateKey = WalletService.EncryptPrivateKey(wallet.PrivateKey, wallet.Id.ToString("N"));
        _walletRepository.Add(wallet);
        await _walletRepository.SaveAsync(cancellationToken);

        var model = await _walletRepository.Get(wallet.Id).SingleAsync<Wallet, WalletView>(cancellationToken);
        return model;
    }
}