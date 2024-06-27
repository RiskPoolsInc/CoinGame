using App.Data.Entities.Games;
using App.Data.Entities.Payments;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class GameConfiguration : EntityTypeConfiguration<Game> {
    public override void Configure(EntityTypeBuilder<Game> builder) {
        builder.HasOne(a => a.Wallet)
               .WithMany(a => a.Games)
               .HasForeignKey(a => a.WalletId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.State)
               .WithMany()
               .HasForeignKey(a => a.StateId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Result)
               .WithMany()
               .HasForeignKey(a => a.ResultId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}