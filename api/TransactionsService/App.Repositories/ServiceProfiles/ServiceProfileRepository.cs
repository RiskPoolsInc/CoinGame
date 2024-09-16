using App.Data.Entities.ServiceProfiles;
using App.Interfaces.Repositories.ServiceProfiles;

namespace App.Repositories.ServiceProfiles;

public class ServiceProfileRepository : ArchivableRepository<ServiceProfile>, IServiceProfileRepository {
    public ServiceProfileRepository(IAppDbContext context) : base(context) {
    }
}