using App.Data.Entities.Notifications;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class NotificationConfiguration : EntityTypeConfiguration<Notification> {
    public override void Configure(EntityTypeBuilder<Notification> builder) {
        builder.HasOne(m => m.CreatedBy)
               .WithMany()
               .HasForeignKey(m => m.CreatedById)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Recipient)
               .WithMany()
               .HasForeignKey(m => m.RecipientId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(m => m.Type).WithMany().HasForeignKey(m => m.TypeId).IsRequired().OnDelete(DeleteBehavior.Restrict);
    }
}