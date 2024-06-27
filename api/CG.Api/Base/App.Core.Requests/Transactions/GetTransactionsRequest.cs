using App.Core.ViewModels.Transactions;

namespace App.Core.Requests.Transactions;

[CustomerAccess]
public class GetTransactionsRequest : IPagedRequest, IRequest<IPagedList<TransactionView>> {
    public string Sort { get; set; } = "CreatedOn";
    public int Direction { get; set; } = 0;
    public int? Page { get; set; }
    public int? Size { get; set; }
    public bool? GetAll { get; set; }
    public bool? WithDeleted { get; set; }
    public int? Skip { get; set; }
}