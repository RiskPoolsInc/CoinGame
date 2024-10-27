using System.Security.Cryptography;
using System.Text;
using App.Common.Helpers;
using App.Core.Commands.GameRounds;
using App.Core.Commands.Games;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Data.Entities.Games;
using App.Interfaces.Core;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Games;

public class RunGameHandler : IRequestHandler<RunGameCommand, GameView>
{
    private readonly IWalletRepository _walletRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameRoundRepository _gameRoundRepository;
    private readonly ITransactionGameDepositRepository _gameDepositRepository;
    private readonly IWalletService _walletService;
    private readonly IDispatcher _dispatcher;

    public RunGameHandler(IWalletRepository walletRepository,
        IGameRepository gameRepository,
        IGameRoundRepository gameRoundRepository,
        ITransactionGameDepositRepository gameDepositRepository,
        IWalletService walletService,
        IDispatcher dispatcher)
    {
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _gameRoundRepository = gameRoundRepository;
        _gameDepositRepository = gameDepositRepository;
        _walletService = walletService;
        _dispatcher = dispatcher;
    }

    public async Task<GameView> Handle(RunGameCommand request, CancellationToken cancellationToken)
    {
        var currentGameId = await _gameRepository
            .Where(a => a.WalletId == request.WalletId && a.StateId == (int)GameStateTypes.Created)
            .Select(a => a.Id)
            .SingleAsync(cancellationToken);

        var currentGame = await _gameRepository.FindAsync(currentGameId, cancellationToken);

        if (currentGame.StateId != (int)GameStateTypes.Created)
            throw new Exception($"Game can't run. Game state is {(GameStateTypes)currentGame.StateId:G}");

        var depositTransaction = await _dispatcher.Send(new CheckGameDepositTransactionCommand(currentGameId), cancellationToken);

        if (depositTransaction.State.Id != (int)TransactionStateTypes.Completed)
            throw new Exception("Transaction to start game in progress");

        currentGame.StateId = (int)GameStateTypes.InProgress;
        await _gameRepository.SaveAsync(cancellationToken);

        await RunGame(currentGame);

        return await _gameRepository.Get(currentGameId).SingleAsync<Game, GameView>(cancellationToken);
    }

    private async Task RunGame(Game currentGame)
    {
        var currentGameId = currentGame.Id;
        var betMultiplier = 1;

        for (var i = 0; i < currentGame.RoundQuantity; i++)
        {
            var roundNumber = i + 1;
            await RandomDelay(500, 1000);
            var randomNumber = await GenerateNextRandomNumber();

            var randomNumberHash = CalculateHash(randomNumber);
            var roundResult = (randomNumber % 2) == 0 ? GameRoundResultTypes.Lose : GameRoundResultTypes.Win;

            if (roundResult == GameRoundResultTypes.Win)
                betMultiplier++;
            else
                betMultiplier--;

            await _dispatcher.Send(new CreateGameRoundCommand(currentGameId, randomNumber, randomNumberHash,
                roundResult,
                betMultiplier * currentGame.RoundSum, roundNumber));

            if (betMultiplier <= 0)
            {
                await SetGameIsLose(currentGameId);
                return;
            }
        }

        await SetGameIsWin(betMultiplier, currentGameId);
    }

    private async Task SetGameIsLose(Guid currentGameId)
    {
        var currentGame = await CompleteGameWithResult(currentGameId, GameResultTypes.Lose);
        _gameRepository.Update(currentGame);
        await _gameRepository.SaveAsync(default);
    }

    private async Task SetGameIsWin(int gameCounter, Guid currentGameId)
    {
        var currentGame = await CompleteGameWithResult(currentGameId, GameResultTypes.Win);
        currentGame.RewardSum = currentGame.RoundSum * gameCounter;
        _gameRepository.Update(currentGame);
        await _gameRepository.SaveAsync(default);
    }

    private async Task<Game> CompleteGameWithResult(Guid gameId, GameResultTypes gameResult)
    {
        var game = await _gameRepository.FindAsync(gameId, default);
        game.StateId = (int)GameStateTypes.Completed;
        game.ResultId = (int)gameResult;
        return game;
    }

    private async Task RandomDelay(int from, int to)
    {
        var random = new Random();
        var next = random.Next(from, to);
        await Task.Delay(next);
    }

    private async Task<int> GenerateNextRandomNumber()
    {
        var firstRandomNumber = GenerateRandomNumber;
        await RandomDelay(1800, 2300);
        var secondRandomNumber = GenerateRandomNumber;
        await RandomDelay(1800, 2300);

        var orderedRandomNumbers = new[]
            {
                firstRandomNumber,
                secondRandomNumber
            }.OrderBy(a => a)
            .ToArray();
        await RandomDelay(50, 100);

        var generatedRandomNumber = RandomNumberGenerator.GetInt32(orderedRandomNumbers[0], orderedRandomNumbers[1]);
        return generatedRandomNumber;
    }

    private string CalculateHash(int number)
    {
        using var hashInst = SHA256.Create();
        var hash = Convert.ToHexString(hashInst.ComputeHash(Encoding.UTF8.GetBytes(number.ToString())));
        return hash.ToLower();
    }

    private int GenerateRandomNumber => RandomNumberGenerator.GetInt32(1, 1000000);
}