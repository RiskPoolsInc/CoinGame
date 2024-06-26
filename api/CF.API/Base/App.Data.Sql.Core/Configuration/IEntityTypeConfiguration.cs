using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.Core.Configuration;

public interface IEntityTypeConfiguration {
    void Configure(ModelBuilder modelBuilder);
}