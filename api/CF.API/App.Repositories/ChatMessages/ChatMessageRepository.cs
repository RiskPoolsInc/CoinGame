using App.Data.Entities.ChatMessages;
using App.Interfaces.Repositories.ChatMessages;

namespace App.Repositories.ChatMessages;

public class ChatMessageRepository : ArchivableRepository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(IAppDbContext context) : base(context)
    {
    }
}