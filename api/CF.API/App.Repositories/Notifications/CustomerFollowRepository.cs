using App.Data.Entities.Notifications;

namespace App.Repositories.Notifications; 

public class CustomerFollowRepository : Repository<CustomerFollow>, ICustomerFollowRepository {
    public CustomerFollowRepository(IAppDbContext context) : base(context) {
    }
}