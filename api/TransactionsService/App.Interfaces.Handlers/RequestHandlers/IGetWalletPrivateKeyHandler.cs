using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;

using MediatR;

namespace App.Interfaces.Handlers.RequestHandlers;

public interface IGetWalletPrivateKeyHandler : IRequestHandler<GetWalletPrivateKeyRequest, WalletPrivateKeyView> {
}