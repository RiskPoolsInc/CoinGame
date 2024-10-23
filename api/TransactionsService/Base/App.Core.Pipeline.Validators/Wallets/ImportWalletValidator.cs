using System.Text.RegularExpressions;

using App.Core.Commands.Wallets;
using App.Data.Criterias.Wallets;
using App.Interfaces.Repositories.Wallets;

using FluentValidation;

namespace App.Core.Pipeline.Validators.Wallets;

public class ImportWalletValidator : AbstractValidator<CreateImportedWalletCommand> {
    private readonly IWalletRepository _walletRepository;

    public ImportWalletValidator(IWalletRepository walletRepository) {
        _walletRepository = walletRepository;
        RuleFor(a => a.Address).NotEmpty().WithMessage("Please specify a valid address.");
        RuleFor(a => a.Address).MustAsync(UniqueAddress).WithMessage("Address should be unique.");
        RuleFor(a => a.Address).MustAsync(StartAddress).WithMessage("Address should be start with Ux.");
        RuleFor(a => a.Address).Must(a => a.Length == 42).WithMessage("Address format incorrect. Count characters must be 42 characters.");
        RuleFor(a => a.PrivateKey).NotEmpty().WithMessage("Please specify a private key.");
        RuleFor(a => a.PrivateKey).Must(a => a.Length == 64).WithMessage("Private key length must be 64 characters.");
        RuleFor(a => a.PrivateKey).Must(FormatPrivateKey).WithMessage("Private key has incorrect symbols.");
    }

    private bool FormatPrivateKey(string arg) {
        var replacedWordsNumbers = new Regex(@"\w+|\d+").Replace(arg, "");
        return replacedWordsNumbers.Length == 0;
    }

    private async Task<bool> StartAddress(string address, CancellationToken cancellationToken) {
        return address.StartsWith("Ux");
    }

    private async Task<bool> UniqueAddress(string address, CancellationToken cancellationToken) {
        var countEqualAddresses = await _walletRepository.CountAsync(new WalletByAddress(address), cancellationToken);
        return countEqualAddresses == 0;
    }
}