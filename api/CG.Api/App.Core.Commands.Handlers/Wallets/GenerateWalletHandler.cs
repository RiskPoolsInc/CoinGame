using App.Core.Commands.Wallets;
using App.Core.ViewModels.External;

namespace App.Core.Commands.Handlers.Wallets;

public class GenerateWalletHandler : IRequestHandler<GenerateWalletCommand, GeneratedWalletView> {
    public Task<GeneratedWalletView> Handle(GenerateWalletCommand request, CancellationToken cancellationToken) {
        throw new NotImplementedException();
    }
}