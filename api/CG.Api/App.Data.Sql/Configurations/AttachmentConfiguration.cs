using App.Data.Entities.Attachments;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class AttachmentConfiguration : EntityTypeConfiguration<Attachment> {
    public override void Configure(EntityTypeBuilder<Attachment> builder) {
        builder.HasOne(m => m.CreatedByUser)
               .WithMany()
               .HasForeignKey(m => m.CreatedByUserId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.ObjectType)
               .WithMany()
               .HasForeignKey(m => m.ObjectTypeId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        builder.Property(m => m.Description).IsRequired(false);

        builder.HasOne(m => m.Type)
               .WithMany()
               .HasForeignKey(m => m.TypeId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.Property(a => a.TypeId).HasDefaultValue(1);

        builder.HasIndex(a => a.ObjectId);

        builder.Property(m => m.FileName).HasMaxLength(300);
    }
}