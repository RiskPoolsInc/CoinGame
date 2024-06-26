using App.Data.Entities.Payments;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionPaymentConfiguration : EntityTypeConfiguration<Transaction> {
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
    }
}