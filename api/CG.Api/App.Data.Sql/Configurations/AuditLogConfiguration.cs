using App.Data.Entities;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class AuditLogConfiguration : EntityTypeConfiguration<AuditLog> {
    public override void Configure(EntityTypeBuilder<AuditLog> builder) {
        builder.HasOne(m => m.CreatedBy).WithMany().HasForeignKey(m => m.CreatedById).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(m => m.Type).WithMany().HasForeignKey(m => m.TypeId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        builder.Property(m => m.IP).IsRequired().HasMaxLength(40).IsUnicode(false);
    }
}