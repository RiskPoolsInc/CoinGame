using App.Data.Entities.Transactions;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionConfiguration : EntityTypeConfiguration<ATransaction> {
    public override void Configure(EntityTypeBuilder<ATransaction> builder) {
        builder.HasOne(a => a.Type)
               .WithMany()
               .HasForeignKey(a => a.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasDiscriminator(a => a.TypeId)
               .HasValue<Transaction>(1)
               .HasValue<TransactionRefund>(2);

        builder.HasOne(a => a.State)
               .WithMany()
               .HasForeignKey(a => a.StateId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Wallet)
               .WithMany(a => a.Transactions)
               .HasForeignKey(a => a.WalletId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}