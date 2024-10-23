using App.Data.Entities.Transactions;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionGameDepositConfiguration : EntityTypeConfiguration<TransactionGameDeposit> {
    public override void Configure(EntityTypeBuilder<TransactionGameDeposit> builder) {
        builder.HasOne(a => a.Game)
               .WithMany(a => a.TransactionGameDeposits)
               .HasForeignKey(a => a.GameId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}