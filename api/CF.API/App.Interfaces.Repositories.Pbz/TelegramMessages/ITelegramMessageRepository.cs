using App.Data.Entities.Pbz.TelegramMessages;

namespace App.Interfaces.Repositories.Pbz.TelegramMessages;

public interface ITelegramMessageRepository : IAuditableRepository<TelegramMessage>
{
}