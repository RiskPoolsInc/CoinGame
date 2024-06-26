using App.Interfaces.Repositories.Settings;

namespace App.Repositories.Settings;

public class SettingPropertyTypeRepository : DictionaryRepository<SettingPropertyType>, ISettingPropertyTypeRepository {
    public SettingPropertyTypeRepository(IAppDbContext context) : base(context) {
    }
}