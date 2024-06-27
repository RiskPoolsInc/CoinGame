using Microsoft.EntityFrameworkCore.Infrastructure;

namespace App.Data.Sql.Core.Interfaces; 

public interface IDatabaseFacadeProvider {
    DatabaseFacade Database { get; }
}