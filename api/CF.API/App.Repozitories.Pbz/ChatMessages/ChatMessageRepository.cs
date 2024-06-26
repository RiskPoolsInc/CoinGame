using App.Data.Entities.Pbz.ChatMessages;
using App.Interfaces.Repositories.Pbz.ChatMessages;

namespace App.Repozitories.Pbz.ChatMessages;

public class ChatMessageRepository : ArchivableRepository<ChatMessage>, IChatMessageRepository
{
    public ChatMessageRepository(IPbzDbContext context) : base(context)
    {
    }
}