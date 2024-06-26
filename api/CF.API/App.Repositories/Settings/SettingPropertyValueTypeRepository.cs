using App.Interfaces.Repositories.Settings;

namespace App.Repositories.Settings;

public class SettingPropertyValueTypeRepository : DictionaryRepository<SettingPropertyValueType>, ISettingPropertyValueTypeRepository {
    public SettingPropertyValueTypeRepository(IAppDbContext context) : base(context) {
    }
}