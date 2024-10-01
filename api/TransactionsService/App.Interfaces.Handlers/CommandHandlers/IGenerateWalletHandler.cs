using App.Core.Commands.Wallets;
using App.Core.ViewModels.Wallets;

using MediatR;

namespace App.Interfaces.Handlers.CommandHandlers;

public interface IGenerateWalletHandler : IRequestHandler<GenerateWalletCommand, WalletView> {
}