using App.Common.Helpers;
using App.Core.Enums;
using App.Core.Requests.Games;
using App.Core.ViewModels.Games;
using App.Data.Entities.Games;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Services.WalletService;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Requests.Handlers.Games;

public class GetGameIsPreparedToRunHandler : IRequestHandler<GetGameIsPreparedToRunRequest, GameIsPreparedToRunView> {
    private readonly ITransactionGameDepositRepository _transactionGameDepositRepository;
    private readonly IWalletService _walletService;
    private readonly IGameRepository _gameRepository;

    public GetGameIsPreparedToRunHandler(ITransactionGameDepositRepository transactionGameDepositRepository,
                                         IWalletService                    walletService, IGameRepository gameRepository) {
        _transactionGameDepositRepository = transactionGameDepositRepository;
        _walletService = walletService;
        _gameRepository = gameRepository;
    }

    public async Task<GameIsPreparedToRunView> Handle(GetGameIsPreparedToRunRequest request, CancellationToken cancellationToken) {
        var game = await _gameRepository.Where(a => a.WalletId == request.WalletId && a.StateId == (int)GameStateTypes.Created)
                                        .SingleAsync<Game, GameView>(cancellationToken);

        var hash = await _transactionGameDepositRepository
                        .Where(a => a.Game.WalletId == request.WalletId && a.Game.StateId == (int)GameStateTypes.Created)
                        .Select(s => s.TransactionHash)
                        .SingleAsync();
        var transactionState = await _walletService.CheckTransactionIsCompleted(hash);

        return new GameIsPreparedToRunView {
            Id = game.Id,
            WalletId = request.WalletId,
            IsPreparedToRun = transactionState.IsCompleted
        };
    }
}