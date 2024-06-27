using App.Core.ViewModels.Games;
using App.Core.ViewModels.Transactions;

namespace App.Core.Commands.Games;

public class CreateGameCommand : IRequest<TransactionGameDepositView> {
    public Guid WalletId { get; set; }
    public int Rounds { get; set; }
    public decimal Rate { get; set; }//ставка
}