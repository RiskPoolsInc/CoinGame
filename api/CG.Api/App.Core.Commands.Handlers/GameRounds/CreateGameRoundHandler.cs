using App.Common.Helpers;
using App.Core.Commands.GameRounds;
using App.Core.ViewModels.Games;
using App.Data.Entities.GameRounds;
using App.Interfaces.Repositories.Dictionaries;
using App.Interfaces.Repositories.Games;

namespace App.Core.Commands.Handlers.GameRounds;

public class CreateGameRoundHandler : IRequestHandler<CreateGameRoundCommand, GameRoundView> {
    private readonly IGameRoundRepository _gameRoundRepository;

    public CreateGameRoundHandler(IGameRoundRepository gameRoundRepository) {
        _gameRoundRepository = gameRoundRepository;
    }

    public async Task<GameRoundView> Handle(CreateGameRoundCommand request, CancellationToken cancellationToken) {
        var gameRoundResult = new GameRound {
            GameId = request.GameId,
            Number = request.GeneratedNumber,
            GeneratedValue = $"{request.GeneratedNumber}",
            ResultId = request.Result,
            RoundNumber = request.RoundNumber,
            CurrentGameRoundSum = request.CurrentGameRoundSum,
            HashForNumber = request.GeneratedNumberHash
        };
        _gameRoundRepository.Add(gameRoundResult);
        await _gameRoundRepository.SaveAsync(default);
        return await _gameRoundRepository.Get(gameRoundResult.Id).SingleAsync<GameRound, GameRoundView>(cancellationToken);
    }
}