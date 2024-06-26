using App.Data.Entities.TelegramMessages;

namespace App.Interfaces.Repositories.TelegramMessages;

public interface IExternalTelegramMessageRepository : IAuditableRepository<ExternalTelegramMessage> {
}