using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;

using MediatR;

namespace App.Interfaces.Handlers.RequestHandlers;

public interface IGetWalletHandler : IRequestHandler<GetWalletRequest, WalletView> {
}