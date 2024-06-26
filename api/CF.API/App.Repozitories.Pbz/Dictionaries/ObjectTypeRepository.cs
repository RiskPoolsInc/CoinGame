namespace App.Repozitories.Pbz.Dictionaries {

public class ObjectTypeRepository : DictionaryRepository<ObjectType>, IObjectTypeRepository
{
    public ObjectTypeRepository(IPbzDbContext context) : base(context) { }
}
}