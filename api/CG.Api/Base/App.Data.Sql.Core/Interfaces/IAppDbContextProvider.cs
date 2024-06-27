using Microsoft.EntityFrameworkCore;

namespace App.Data.Sql.Core.Interfaces; 

public interface IAppDbContextProvider<TContext> where TContext : BaseDbContext {
    DbContextOptions<TContext> GetOptions();
}