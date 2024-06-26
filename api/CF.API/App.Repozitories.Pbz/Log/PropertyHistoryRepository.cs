using App.Data.Entities.Pbz.PropertyHistories;
using App.Interfaces.Repositories.Pbz.Log;

namespace App.Repozitories.Pbz.Log;

public class PropertyHistoryRepository : Repository<PropertyHistory>, IPropertyHistoryRepository
{
    public PropertyHistoryRepository(IPbzDbContext context) : base(context)
    {
    }
}