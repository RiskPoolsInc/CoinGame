namespace App.Core.ViewModels.External;

public class TransactionReceiverView : BaseView {
    public string Address { get; set; }
    public string AddressFrom { get; set; }
    public decimal Sum { get; set; }
}