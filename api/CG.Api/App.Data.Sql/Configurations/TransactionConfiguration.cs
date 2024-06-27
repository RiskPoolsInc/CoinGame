using App.Data.Entities.Payments;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionConfiguration : EntityTypeConfiguration<Transaction> {
    public override void Configure(EntityTypeBuilder<Transaction> builder) {
        builder.HasOne(a => a.Type)
               .WithMany()
               .HasForeignKey(a => a.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasDiscriminator(a => a.TypeId)
               .HasValue<TransactionCreateGame>(1)
               .HasValue<TransactionUserReward>(2)
               .HasValue<TransactionUserRefund>(3)
               .HasValue<TransactionService>(4)
            ;

        builder.HasOne(a => a.State)
               .WithMany()
               .HasForeignKey(a => a.StateId)
               .OnDelete(DeleteBehavior.Restrict);
        ;

        builder.HasOne(a => a.WalletFrom)
               .WithMany()
               .HasForeignKey(a => a.WalletFromId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}