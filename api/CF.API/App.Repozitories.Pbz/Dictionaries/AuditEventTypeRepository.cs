using App.Interfaces.Repositories.Pbz.Dictionaries;

namespace App.Repozitories.Pbz.Dictionaries {

public class AuditEventTypeRepository : DictionaryRepository<AuditEventType>, IAuditEventTypeRepository
{
    public AuditEventTypeRepository(IPbzDbContext context) : base(context)
    {
    }
}
}