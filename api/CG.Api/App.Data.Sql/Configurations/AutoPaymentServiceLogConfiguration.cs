using App.Data.Entities.AutoPaymentServiceLogs;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class AutoPaymentServiceLogConfiguration : EntityTypeConfiguration<AutoPaymentServiceLog> {
    public override void Configure(EntityTypeBuilder<AutoPaymentServiceLog> builder) {
        builder.HasOne(a => a.Type)
               .WithMany()
               .HasForeignKey(a => a.TypeId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}