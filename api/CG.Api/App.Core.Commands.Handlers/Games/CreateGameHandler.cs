using App.Core.Commands.Games;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Games;
using App.Core.ViewModels.Transactions;
using App.Data.Entities.Games;
using App.Interfaces.Core;
using App.Interfaces.Repositories.Games;

namespace App.Core.Commands.Handlers.Games;

public class CreateGameHandler : IRequestHandler<CreateGameCommand, TransactionGameDepositView> {
    private readonly IGameRepository _gameRepository;
    private readonly IDispatcher _dispatcher;

    public CreateGameHandler(IGameRepository gameRepository, IDispatcher dispatcher) {
        _gameRepository = gameRepository;
        _dispatcher = dispatcher;
    }

    public async Task<TransactionGameDepositView> Handle(CreateGameCommand request, CancellationToken cancellationToken) {
        var game = new Game {
            WalletId = request.WalletId,
            StateId = (int)GameStateTypes.Created,
            State = null,
            ResultId = (int)GameResultTypes.Undefined,
            RoundQuantity = request.Rounds,
            RoundSum = request.Rate
        };
        _gameRepository.Add(game);
        await _gameRepository.SaveAsync(cancellationToken);

        try {
            var transaction = await _dispatcher.Send(new GameDepositTransactionCommand() {
                WalletId = game.WalletId,
                Sum = game.RoundSum,
                GameId = game.Id
            }, cancellationToken);
            return transaction;
        }
        catch (Exception e) {
            _gameRepository.Delete(game.Id);
            await _gameRepository.SaveAsync(cancellationToken);

            throw;
        }
    }
}