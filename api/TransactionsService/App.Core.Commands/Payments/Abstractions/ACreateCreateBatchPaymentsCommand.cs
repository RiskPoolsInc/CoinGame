using App.Core.Commands.Payments.Interfaces;

namespace App.Core.Commands.Payments.Abstractions;

public abstract class ACreateCreateBatchPaymentsCommand<T> : ICreateBatchPaymentCommand<T> where T : ICreateItemPaymentCommand {
    public string AddressTo { get; set; }
    public Guid? UserId { get; set; }
    public Guid? UbikiriUserId { get; set; }

    public decimal Sum { get; set; }
    public T[] Items { get; set; }
}