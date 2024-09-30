using App.Data.Entities.Transactions;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class TransactionUserRewardConfiguration : EntityTypeConfiguration<TransactionUserReward> {
    public override void Configure(EntityTypeBuilder<TransactionUserReward> builder) {
        builder.HasOne(a => a.Game)
               .WithMany(a => a.TransactionUserRewards)
               .HasForeignKey(a => a.GameId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}