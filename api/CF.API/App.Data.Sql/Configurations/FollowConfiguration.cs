using App.Data.Entities.Notifications;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class FollowConfiguration : EntityTypeConfiguration<Follow> {
    public override void Configure(EntityTypeBuilder<Follow> builder) {
        builder.HasOne(m => m.Follower)
               .WithMany()
               .HasForeignKey(m => m.FollowerId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);
        
        builder.HasOne(m => m.Type).WithMany().HasForeignKey(m => m.TypeId).IsRequired().OnDelete(DeleteBehavior.Restrict);

        builder.HasDiscriminator(a => a.TypeId)
               .HasValue<UserFollow>(1)
            ;
    }
}