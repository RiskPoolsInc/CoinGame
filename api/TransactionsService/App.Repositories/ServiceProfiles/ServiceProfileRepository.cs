using App.Data.Entities.Senders;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.ServiceProfiles;

namespace App.Repositories.ServiceProfiles;

public class ServiceProfileRepository: ArchivableRepository<ServiceProfile>, IServiceProfileRepository {
    public ServiceProfileRepository(IAppDbContext context) : base(context) {
    }
}