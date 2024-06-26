using App.Data.Entities.Settings;
using App.Data.Sql.Core.Interfaces;
using App.Interfaces.Repositories.Settings;

namespace App.Repositories.Settings;

public class SettingPropertyRepository : Repository<SettingProperty>, ISettingPropertyRepository {
    public SettingPropertyRepository(IDbContext context) : base(context) {
    }
}