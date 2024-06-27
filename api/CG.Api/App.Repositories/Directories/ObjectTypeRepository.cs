using App.Interfaces.Repositories;

namespace App.Repositories.Directories; 

public class ObjectTypeRepository : DictionaryRepository<ObjectType>, IObjectTypeRepository {
    public ObjectTypeRepository(IAppDbContext context) : base(context) {
    }
}