using App.Core.Commands.Wallets;
using App.Core.ViewModels.Wallets;
using App.Interfaces.Core;
using App.Services.WalletService;

namespace App.Core.Commands.Handlers.Wallets;

public class GenerateWalletHandler : IRequestHandler<GenerateWalletCommand, WalletView> {
    private readonly IWalletService _walletService;
    private readonly IDispatcher _dispatcher;
    private readonly IMapper _mapper;

    public GenerateWalletHandler(IWalletService walletService, IDispatcher dispatcher, IMapper mapper) {
        _walletService = walletService;
        _dispatcher = dispatcher;
        _mapper = mapper;
    }

    public async Task<WalletView> Handle(GenerateWalletCommand request, CancellationToken cancellationToken) {
        var generatedWallet = await _walletService.GenerateWallet();
        var command = _mapper.Map<CreateWalletCommand>(generatedWallet);
        return await _dispatcher.Send(command);
    }
}