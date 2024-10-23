using App.Core.ViewModels.External;

namespace App.Core.Commands.TransactionReceivers;

public class CreateTransactionReceiversCommand : IRequest<TransactionReceiverView> {
    public Guid TransactionId { get; set; }
    public CreateTransactionReceiverCommand[] Receivers { get; set; }
}