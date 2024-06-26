using App.Data.Sql.Core.Interfaces;
using App.Services.RestoreUsers.ConnectionStringProviders;

using Microsoft.Extensions.Configuration;

namespace App.Services.RestoreUsers.DbContextes;

public class PBZContextFactory : BaseContextFactory {
    protected override IConnectionStringProvider GetConnectionStringProvider(IConfiguration configuration) {
        return new PBZConnectionStringProvider(configuration);
    }
}