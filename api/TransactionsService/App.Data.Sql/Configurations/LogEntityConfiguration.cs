using App.Data.Entities.TransactionLogs;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class LogEntityConfiguration : EntityTypeConfiguration<LogEntity> {
    public override void Configure(EntityTypeBuilder<LogEntity> builder) {
        builder.HasOne(a => a.Type)
               .WithMany()
               .HasForeignKey(a => a.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Wallet)
               .WithMany()
               .HasForeignKey(a => a.WalletId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Sender)
               .WithMany()
               .HasForeignKey(a => a.SenderId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}