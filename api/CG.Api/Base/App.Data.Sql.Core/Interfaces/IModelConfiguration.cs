using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.Core.Interfaces; 

public interface IModelConfiguration {
    void Configure(ModelBuilder modelBuilder);
}