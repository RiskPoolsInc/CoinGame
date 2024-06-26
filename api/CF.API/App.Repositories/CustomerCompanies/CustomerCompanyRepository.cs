using App.Data.Entities.Companies;
using App.Interfaces.Repositories;

namespace App.Repositories.CustomerCompanies;

public class CustomerCompanyRepository : ArchivableRepository<CustomerCompany>, ICustomerCompanyRepository {
    public CustomerCompanyRepository(IAppDbContext context) : base(context) {
    }
}