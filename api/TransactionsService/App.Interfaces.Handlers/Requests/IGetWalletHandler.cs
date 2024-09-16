using App.Core.Requests.Wallets;
using App.Core.ViewModels.Wallets;

using MediatR;

namespace App.Interfaces.Handlers.Requests;

public interface IGetWalletHandler : IRequestHandler<GetWalletRequest, WalletView> {
}