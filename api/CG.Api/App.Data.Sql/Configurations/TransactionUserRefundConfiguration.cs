using App.Data.Entities.Transactions;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionUserRefundConfiguration : EntityTypeConfiguration<TransactionUserRefund> {
    public override void Configure(EntityTypeBuilder<TransactionUserRefund> builder) {
        builder.HasOne(a => a.Game)
               .WithMany(a => a.TransactionUserRefunds)
               .HasForeignKey(a => a.GameId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}