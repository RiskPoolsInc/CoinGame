using App.Data.Entities.Wallets;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class WalletConfiguration : EntityTypeConfiguration<Wallet> {
    public override void Configure(EntityTypeBuilder<Wallet> builder) {
    }
}