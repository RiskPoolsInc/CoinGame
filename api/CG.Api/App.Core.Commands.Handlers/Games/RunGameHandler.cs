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
    private readonly IWalletService _walletService;
    private readonly IDispatcher _dispatcher;

    public RunGameHandler(IWalletRepository                 walletRepository,
                          IGameRepository                   gameRepository,
                          IGameRoundRepository              gameRoundRepository,
                          ITransactionGameDepositRepository gameDepositRepository,
                          IWalletService                    walletService,
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

        await RunGame(currentGame);

        return await _gameRepository.Get(currentGameId).SingleAsync<Game, GameView>(cancellationToken);
    }

    private async Task RunGame(Game currentGame) {
        var currentGameId = currentGame.Id;
        var gameCounter = 1;
        var gameLose = false;

        for (var i = 0; i < currentGame.RoundQuantity; i++) {
            var sequenceNumberRound = i + 1;
            await RandomDelay(500, 1000);
            var nextNumber = await GenerateNextRandomNumber();
            var isEven = (nextNumber % 2) == 0;
            var roundResult = isEven ? GameRoundResultTypes.Lose : GameRoundResultTypes.Win;
            var roundResultIsWin = roundResult == GameRoundResultTypes.Win;
            gameCounter += roundResultIsWin ? 1 : -1;

            await _dispatcher.Send(new CreateGameRoundCommand(currentGameId, nextNumber, roundResult, gameCounter * currentGame.RoundSum,
                sequenceNumberRound));


            if (gameCounter <= 0) {
                currentGame = await _gameRepository.FindAsync(currentGameId, default);
                currentGame.StateId = (int)GameStateTypes.Completed;
                currentGame.ResultId = (int)GameResultTypes.Lose;
                gameLose = true;
                await _gameRepository.SaveAsync(default);

                var transactionService = new GameServiceTransactionComand() {
                    WalletId = currentGame.WalletId,
                    GameId = currentGameId
                };
                await _dispatcher.Send(transactionService);
                break;
            }
        }

        if (!gameLose) {
            currentGame = await _gameRepository.FindAsync(currentGameId, default);
            currentGame.StateId = (int)GameStateTypes.Completed;
            currentGame.ResultId = (int)GameResultTypes.Win;
            currentGame.RewardSum = currentGame.RoundSum * gameCounter;
            await _gameRepository.SaveAsync(default);

            var transactionService = new CreateTransactionRewardCommand(currentGameId);
            await _dispatcher.Send(transactionService);
        }
    }

    private async Task RandomDelay(int from, int to) {
        var random = new Random();
        var next = random.Next(from, to);
        await Task.Delay(next);
    }

    private async Task<int> GenerateNextRandomNumber() {
        var firstRandomNumber = RandomNumberGenerator.GetInt32(2, 5000);
        await RandomDelay(2000, 2500);
        var secondRandomNumber = RandomNumberGenerator.GetInt32(6000, 10000);

        var orderedRandomNumbers = new[] {
                firstRandomNumber,
                secondRandomNumber
            }.OrderBy(a => a)
             .ToArray();
        await RandomDelay(50, 100);

        var generatedRandomNumber = RandomNumberGenerator.GetInt32(orderedRandomNumbers[0], orderedRandomNumbers[1]);
        return generatedRandomNumber;
    }
}