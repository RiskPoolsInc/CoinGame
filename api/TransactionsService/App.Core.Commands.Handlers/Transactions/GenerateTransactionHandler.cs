using App.Common.Helpers;
using App.Core.Commands.Transactions;
using App.Core.Enums;
using App.Core.ViewModels.Transactions;
using App.Data.Criterias.Wallets;
using App.Data.Entities.TransactionReceivers;
using App.Data.Entities.Transactions;
using App.Interfaces.Repositories.Transactions;
using App.Interfaces.Repositories.Wallets;
using App.Services.WalletService;
using App.Services.WalletService.Models;
using TransactionReceiverModel = App.Services.WalletService.Models.TransactionReceiverModel;
using Microsoft.EntityFrameworkCore;

namespace App.Core.Commands.Handlers.Transactions;

public class GenerateTransactionHandler : IRequestHandler<GenerateTransactionCommand, TransactionView> {
    private readonly IWalletService _walletService;
    private readonly ITransactionRepository _transactionRepository;
    private readonly ITransactionErrorRepository _transactionErrorRepository;
    private readonly IWalletRepository _walletRepository;
    private readonly IMapper _mapper;

    public GenerateTransactionHandler(IWalletService              walletService, ITransactionRepository transactionRepository,
                                      ITransactionErrorRepository transactionErrorRepository,
                                      IWalletRepository           walletRepository, IMapper mapper) {
        _walletService = walletService;
        _transactionRepository = transactionRepository;
        _transactionErrorRepository = transactionErrorRepository;
        _walletRepository = walletRepository;
        _mapper = mapper;
    }

    public async Task<TransactionView> Handle(GenerateTransactionCommand request, CancellationToken cancellationToken) {
        var wallet = await _walletRepository.Get(request.WalletId)
                                            .SingleAsync(cancellationToken);
        var privateKey = WalletService.DecrypPrivateKey(wallet.PrivateKey, wallet.Id);

        try {
            var receivers = _mapper.Map<TransactionReceiverModel[]>(request.Receivers);

            var generatedTransaction = await _walletService.GenerateTransaction(wallet.Address, privateKey, receivers);

            var transaction = _mapper.Map<Transaction>(generatedTransaction);

            transaction.WalletId = wallet.Id;
            transaction.Address = wallet.Address;
            transaction.StateId = (int)TransactionStateTypes.Created;

            // Hash = generatedTransaction.Hash,
            // Sum = generatedTransaction.Sum,
            // Fee = generatedTransaction.Fee,
 
            _transactionRepository.Add(transaction);

            var transactionReceivers = _mapper.Map<TransactionReceiver[]>(generatedTransaction.Receviers);

            foreach (var transactionReceiver in transactionReceivers)
                transactionReceiver.TransactionId = transaction.Id;

            await _transactionRepository.SaveAsync(default);
            return await _transactionRepository.Get(transaction.Id).SingleAsync<Transaction, TransactionView>(cancellationToken);
        }
        catch (Exception e) {
            var transactionError = await SaveTransactionError(e, wallet.Id, wallet.Address, request.Receivers.Sum(a => a.Sum));
            throw new Exception($"Transaction error id: {transactionError.Id}. {e.Message}");
        }
    }

    private async Task<TransactionError> SaveTransactionError(Exception e, Guid walletFromId, string addressFrom, decimal sum) {
        var errorTransaction = new TransactionError {
            WalletId = walletFromId,
            Address = addressFrom,
            Sum = sum,
            Fee = 0,
            StateId = (int)TransactionStateTypes.NotCreated,
            ExistInBlockChain = false,
            Error = e.Message
        };
        _transactionErrorRepository.Add(errorTransaction);
        await _transactionErrorRepository.SaveAsync(default);
        throw new Exception($"Transaction error id: {errorTransaction.Id}");
    }
}