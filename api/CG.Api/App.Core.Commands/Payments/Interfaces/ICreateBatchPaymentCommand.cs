namespace App.Core.Commands.Payments.Interfaces;

public interface ICreateBatchPaymentCommand<T> where T : ICreateItemPaymentCommand {
    string AddressTo { get; set; }
    Guid? UserId { get; set; }
    Guid? UbikiriUserId { get; set; }
    decimal Sum { get; set; }
    T[] Items { get; set; }
}