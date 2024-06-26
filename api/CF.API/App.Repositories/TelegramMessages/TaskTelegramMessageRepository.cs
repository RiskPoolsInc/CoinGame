using App.Data.Entities.TelegramMessages;
using App.Interfaces.Repositories.TelegramMessages;

namespace App.Repositories.TelegramMessages;

public class TaskTelegramMessageRepository : AuditableRepository<TaskTelegramMessage>, ITaskTelegramMessageRepository {
    public TaskTelegramMessageRepository(IAppDbContext context) : base(context) {
    }
}