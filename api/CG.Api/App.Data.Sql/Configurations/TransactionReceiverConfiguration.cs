using App.Data.Entities.TransactionReceiver;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionReceiverConfiguration : EntityTypeConfiguration<TransactionReceiver> {
    public override void Configure(EntityTypeBuilder<TransactionReceiver> builder) {
        builder.HasOne(a => a.Type)
               .WithMany()
               .HasForeignKey(a => a.TypeId)
               .OnDelete(DeleteBehavior.Restrict)
            ;
    }
}