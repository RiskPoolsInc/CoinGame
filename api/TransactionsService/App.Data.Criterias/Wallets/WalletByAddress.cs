using System.Linq.Expressions;

using App.Data.Criterias.Core;
using App.Data.Entities.Wallets;

namespace App.Data.Criterias.Wallets;

public class WalletByAddress : ACriteria<Wallet> {
    private readonly string _address;

    public WalletByAddress(string address) {
        _address = address;
    }

    public override Expression<Func<Wallet, bool>> Build() {
        return a => a.Address == _address;
    }
}