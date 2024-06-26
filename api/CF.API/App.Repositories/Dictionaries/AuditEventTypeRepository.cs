namespace App.Repositories.Dictionaries {

public class AuditEventTypeRepository : DictionaryRepository<AuditEventType>, IAuditEventTypeRepository
{
    public AuditEventTypeRepository(IAppDbContext context) : base(context)
    {
    }
}
}