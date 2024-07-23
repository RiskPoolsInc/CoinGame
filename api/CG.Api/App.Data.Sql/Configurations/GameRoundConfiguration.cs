using App.Data.Entities.GameRounds;
using App.Data.Sql.Core.Configuration;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Data.Sql.Configurations;

public class GameRoundConfiguration : EntityTypeConfiguration<GameRound> {
    public override void Configure(EntityTypeBuilder<GameRound> builder) {
        builder.HasOne(a => a.Game)
               .WithMany(s => s.GameRounds)
               .HasForeignKey(a => a.GameId)
               .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(a => a.Result)
               .WithMany()
               .HasForeignKey(a => a.ResultId)
               .OnDelete(DeleteBehavior.Restrict);
        ;
    }
}