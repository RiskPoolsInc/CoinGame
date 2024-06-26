using App.Data.Entities.TelegramMessages;
using App.Interfaces.Repositories.TelegramMessages;

namespace App.Repositories.TelegramMessages;

public class ExternalTelegramMessageRepository : AuditableRepository<ExternalTelegramMessage>, IExternalTelegramMessageRepository {
    public ExternalTelegramMessageRepository(IAppDbContext context) : base(context) {
    }
}