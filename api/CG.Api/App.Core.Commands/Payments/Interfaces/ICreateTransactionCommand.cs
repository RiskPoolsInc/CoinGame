namespace App.Core.Commands.Payments.Interfaces;

public interface ICreateTransactionCommand<T, out U> where T : ICreateBatchPaymentCommand<U> where U : ICreateItemPaymentCommand {
    int? CoinType { get; set; }
    string TransactionHash { get; set; }
    string AddressFrom { get; set; }
    decimal Sum { get; set; }
    decimal Fee { get; set; }
    T[] BatchPayments { get; set; }
}