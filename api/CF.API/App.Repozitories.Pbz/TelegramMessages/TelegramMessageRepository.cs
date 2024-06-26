using App.Data.Entities.Pbz.TelegramMessages;
using App.Interfaces.Repositories.Pbz.TelegramMessages;

namespace App.Repozitories.Pbz.TelegramMessages;

public class TelegramMessageRepository : AuditableRepository<TelegramMessage>, ITelegramMessageRepository
{
    public TelegramMessageRepository(IPbzDbContext context) : base(context)
    {
    }
}