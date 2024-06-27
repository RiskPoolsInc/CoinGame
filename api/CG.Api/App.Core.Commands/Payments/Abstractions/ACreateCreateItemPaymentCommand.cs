using App.Core.Commands.Payments.Interfaces;

namespace App.Core.Commands.Payments.Abstractions;

public abstract class ACreateCreateItemPaymentCommand : ICreateItemPaymentCommand {
    public abstract int TypeId { get; }
    public Guid Id { get; set; }
    public decimal Sum { get; set; }
}