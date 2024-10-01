using App.Core.ViewModels.Dictionaries;
using App.Core.ViewModels.ServiceProfiles;
using App.Core.ViewModels.Wallets;

namespace App.Core.ViewModels.LogEntities;

public class LogEntityView : BaseView {
    public LogTypeView Type { get; set; }
    public ServiceProfileView Sender { get; set; }
    public WalletView Wallet { get; set; }
    public Guid? TransactionId { get; set; }

    public string WalletServiceRequestBody { get; set; }
    public string? WalletServiceResponceBody { get; set; }
    public string? Error { get; set; }
    public string WalletServiceRequestReceiver { get; set; }
}