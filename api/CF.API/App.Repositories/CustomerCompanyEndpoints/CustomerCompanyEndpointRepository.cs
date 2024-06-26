using App.Data.Entities.Companies;
using App.Interfaces.Repositories;

namespace App.Repositories.CustomerCompanyEndpoints;

public class CustomerCompanyEndpointRepository : ArchivableRepository<CustomerCompanyEndpoint>, ICustomerCompanyEndpointRepository {
    public CustomerCompanyEndpointRepository(IAppDbContext context) : base(context) {
    }
}