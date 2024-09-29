using App.Common.Helpers;
using App.Core.Commands.Wallets;
using App.Core.Enums;
using App.Core.ViewModels.Wallets;
using App.Data.Entities.Wallets;
using App.Interfaces.Core;
using App.Interfaces.Repositories.Wallets;

namespace App.Core.Commands.Handlers.Wallets;

public class CreateWalletHandler : IRequestHandler<CreateWalletCommand, WalletView> {
    private readonly IDispatcher _dispatcher;
    private readonly IMapper _mapper;
    private readonly IWalletRepository _walletRepository;

    public CreateWalletHandler(IWalletRepository walletRepository,
                               IDispatcher       dispatcher, IMapper mapper) {
        _walletRepository = walletRepository;
        _dispatcher = dispatcher;
        _mapper = mapper;
    }

    public async Task<WalletView> Handle(CreateWalletCommand request, CancellationToken cancellationToken) {
        var generatedWallet = await _dispatcher.Send(new GenerateWalletCommand());
        var wallet = _mapper.Map<Wallet>(generatedWallet);

        if (_walletRepository.Any(a => a.Id == generatedWallet.Id))
            wallet.Id = Guid.NewGuid();

        wallet.TypeId = (int)WalletTypes.Game;
        _walletRepository.Add(wallet);
        await _walletRepository.SaveAsync(cancellationToken);

        var model = await _walletRepository.Get(wallet.Id).SingleAsync<Wallet, WalletView>(cancellationToken);
        return model;
    }
}