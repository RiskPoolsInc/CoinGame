using App.Data.Entities.TransactionReceivers;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionReceiverConfiguration : EntityTypeConfiguration<TransactionReceiver> {
    public override void Configure(EntityTypeBuilder<TransactionReceiver> builder) {
        builder.HasOne(a => a.Type)
               .WithMany()
               .HasForeignKey(a => a.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.ATransaction)
               .WithMany(a => a.Receivers)
               .HasForeignKey(a => a.TransactionId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}