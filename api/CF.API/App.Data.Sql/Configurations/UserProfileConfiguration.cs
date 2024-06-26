 using App.Data.Entities.UserProfiles;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class UserProfileConfiguration : EntityTypeConfiguration<UserProfile> {
    public override void Configure(EntityTypeBuilder<UserProfile> builder)
    {
        builder.HasIndex(a => a.UbikiriUserId).IsUnique();
        builder.HasOne(a => a.Type).WithMany().HasForeignKey(a => a.TypeId).OnDelete(DeleteBehavior.Restrict);
    }
}