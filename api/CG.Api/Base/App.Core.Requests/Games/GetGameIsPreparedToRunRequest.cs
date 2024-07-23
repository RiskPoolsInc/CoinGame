using App.Core.ViewModels.Games;

namespace App.Core.Requests.Games;

public class GetGameIsPreparedToRunRequest : IRequest<GameIsPreparedToRunView> {
    public Guid WalletId { get; set; }

    public GetGameIsPreparedToRunRequest() {
        
    }

    public GetGameIsPreparedToRunRequest(Guid walletId) {
        WalletId = walletId;
    }
}