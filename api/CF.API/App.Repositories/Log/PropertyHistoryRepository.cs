using App.Data.Entities.PropertyHistories;
using App.Interfaces.Repositories.Log;

namespace App.Repositories.Log;

public class PropertyHistoryRepository : Repository<PropertyHistory>, IPropertyHistoryRepository
{
    public PropertyHistoryRepository(IAppDbContext context) : base(context)
    {
    }
}