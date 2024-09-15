using App.Core.Commands.Wallets;
using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Handlers;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

namespace App.Core.Commands.Handlers.Wallets;

public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, WalletView>,
                                   IRequestHandler<CreateImportedWalletCommand, WalletView>,
                                   IRequestHandler<CreateGeneratedWalletCommand, WalletView> {
    private readonly IMapper _mapper;
    private readonly IWalletRepository _walletRepository;
    private readonly IGetWalletHandler _getWalletHandler;

    public CreateWalletHandler(IMapper mapper, IWalletRepository walletRepository, IGetWalletHandler getWalletHandler) {
        _mapper = mapper;
        _walletRepository = walletRepository;
        _getWalletHandler = getWalletHandler;
    }

    public async Task<WalletView> Handle(CreateWalletCommand request, CancellationToken cancellationToken) {
        var wallet = _mapper.Map<Wallet>(request);
        _walletRepository.Add(wallet);
        wallet.PrivateKey = WalletService.EncryptPrivateKey(wallet.PrivateKey, wallet.Id);
        await _walletRepository.SaveAsync(cancellationToken);

        var model = await _getWalletHandler.Handle(new GetWalletRequest(wallet.Id), cancellationToken);
        return model;
    }

    public Task<WalletView> Handle(CreateImportedWalletCommand request, CancellationToken cancellationToken) {
        return this.Handle(request as CreateWalletCommand, cancellationToken);
    }

    public Task<WalletView> Handle(CreateGeneratedWalletCommand request, CancellationToken cancellationToken) {
        return this.Handle(request as CreateWalletCommand, cancellationToken);
    }
}