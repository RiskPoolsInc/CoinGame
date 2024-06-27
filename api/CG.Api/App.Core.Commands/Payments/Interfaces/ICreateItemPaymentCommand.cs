namespace App.Core.Commands.Payments.Interfaces;

public interface ICreateItemPaymentCommand {
    int TypeId { get; }
    Guid Id { get; set; }
    decimal Sum { get; set; }
}