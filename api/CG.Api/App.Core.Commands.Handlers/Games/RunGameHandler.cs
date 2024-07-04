using System.Security.Cryptography;
using System.Text.RegularExpressions;

using App.Common.Helpers;
using App.Core.Commands.GameRounds;
using App.Core.Commands.Games;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.GameRounds;
using App.Data.Entities.Games;
using App.Interfaces.Core;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Games;

public class RunGameHandler : IRequestHandler<RunGameCommand, GameView> {
    private readonly IWalletRepository _walletRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly ITransactionGameDepositRepository _gameDepositRepository;
    private readonly WalletService _walletService;
    private readonly IDispatcher _dispatcher;

    public RunGameHandler(IWalletRepository                 walletRepository,
                          IGameRepository                   gameRepository,
                          IGameRoundRepository              gameRoundRepository,
                          ITransactionGameDepositRepository gameDepositRepository,
                          WalletService                     walletService,
                          IDispatcher                       dispatcher) {
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _gameRoundRepository = gameRoundRepository;
        _gameDepositRepository = gameDepositRepository;
        _walletService = walletService;
        _dispatcher = dispatcher;
    }

    public async Task<GameView> Handle(RunGameCommand request, CancellationToken cancellationToken) {
        var currentGameId = await _gameRepository.Where(a => a.WalletId == request.WalletId && a.StateId == (int)GameStateTypes.Created)
                                                 .Select(a => a.Id)
                                                 .SingleAsync(cancellationToken);

        var currentGame = await _gameRepository.FindAsync(currentGameId, cancellationToken);


        if (currentGame.StateId == (int)GameStateTypes.Completed)
            throw new Exception("Game was completed");

        if (currentGame.StateId == (int)GameStateTypes.InProgress)
            throw new Exception("Game in Progress");

        var depositTransaction = await _dispatcher.Send(new CheckGameDepositTransactionCommand(currentGameId), cancellationToken);

        if (depositTransaction.State.Id != (int)TransactionStateTypes.Completed)
            throw new Exception("Transaction to start game in progress");

        currentGame.StateId = (int)GameStateTypes.InProgress;
        await _gameRepository.SaveAsync(cancellationToken);
        var gameResult = 0;
        var gameLose = false;

        for (var i = 0; i < currentGame.RoundQuantity; i++) {
            await RandomDelay();
            var nextNumber = NextNumber();
            var isEven = (nextNumber % 2) == 0;
            var roundResult = isEven ? GameRoundResultTypes.Win : GameRoundResultTypes.Lose;
            var roundResultIsWin = roundResult == GameRoundResultTypes.Win;
            await _dispatcher.Send(new CreateGameRoundCommand(currentGameId, nextNumber, roundResult));

            gameResult += roundResultIsWin ? 1 : -1;

            if (gameResult < 0) {
                currentGame = await _gameRepository.FindAsync(currentGameId, cancellationToken);
                currentGame.StateId = (int)GameStateTypes.Completed;
                currentGame.ResultId = (int)GameResultTypes.Lose;
                await _gameRepository.SaveAsync(cancellationToken);

                var transactionService = new GameServiceTransactionComand() {
                    WalletId = currentGame.WalletId,
                    GameId = currentGameId
                };
                await _dispatcher.Send(transactionService);
                break;
            }
        }

        if (!gameLose) {
            currentGame = await _gameRepository.FindAsync(currentGameId, cancellationToken);
            currentGame.StateId = (int)GameStateTypes.Completed;
            currentGame.ResultId = (int)GameResultTypes.Win;
            await _gameRepository.SaveAsync(cancellationToken);

            var transactionService = new CreateTransactionRewardCommand(currentGameId);
            await _dispatcher.Send(transactionService);
        }

        return await _gameRepository.Get(currentGameId).SingleAsync<Game, GameView>(cancellationToken);
    }

    private async Task RandomDelay() {
        var random = new Random();
        var next = random.Next(100, 4500);
        await Task.Delay(next);
    }

    private int NextNumber() {
        var randomNumbersBetween = new[] {
                RandomNumberGenerator.GetInt32(1, 10001),
                RandomNumberGenerator.GetInt32(1, 10001)
            }.OrderBy(a => a)
             .ToArray();

        var result = RandomNumberGenerator.GetInt32(randomNumbersBetween[0], randomNumbersBetween[1]);
        return result;
    }
}