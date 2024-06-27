using Microsoft.EntityFrameworkCore.Infrastructure;

namespace App.Data.Sql.Core.Interfaces; 

public interface IDbFacadeProvider {
    DatabaseFacade Database { get; }
}