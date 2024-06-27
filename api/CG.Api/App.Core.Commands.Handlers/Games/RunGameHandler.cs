using System.Security.Cryptography;
using System.Text.RegularExpressions;

using App.Common.Helpers;
using App.Core.Commands.Games;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Data.Entities.Dictionaries;
using App.Data.Entities.GameRounds;
using App.Data.Entities.Games;
using App.Interfaces.Repositories.Games;
using App.Interfaces.Repositories.Wallets;

using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Games;

public class RunGameHandler : IRequestHandler<RunGameCommand, GameView> {
    private readonly IWalletRepository _walletRepository;
    private readonly IGameRepository _gameRepository;
    private readonly IGameRoundRepository _gameRoundRepository;

    public RunGameHandler(IWalletRepository walletRepository, IGameRepository gameRepository, IGameRoundRepository gameRoundRepository) {
        _walletRepository = walletRepository;
        _gameRepository = gameRepository;
        _gameRoundRepository = gameRoundRepository;
    }

    public async Task<GameView> Handle(RunGameCommand request, CancellationToken cancellationToken) {
        var currentGameId = await _gameRepository.Where(a => a.WalletId == request.WalletId && a.StateId == (int)GameStates.Created)
                                                 .Select(a => a.Id)
                                                 .SingleAsync(cancellationToken);
        var currentGame = await _gameRepository.FindAsync(currentGameId, cancellationToken);
        currentGame.StateId = (int)GameStates.InProgress;
        await _gameRepository.SaveAsync(cancellationToken);

        for (var i = 0; i < currentGame.RoundQuantity; i++) {
            await RandomDelay();
            var nextNumber = NextNumber();
            var isEvent = (nextNumber % 2) == 0;

            var roundResult = new GameRound {
                GameId = currentGameId,
                Number = nextNumber,
                GeneratedValue = null,
                ResultId = 0,
                Result = null
            }
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