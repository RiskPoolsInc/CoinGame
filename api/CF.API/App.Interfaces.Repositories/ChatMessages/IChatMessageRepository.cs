using App.Data.Entities.ChatMessages;

namespace App.Interfaces.Repositories.ChatMessages;

public interface IChatMessageRepository: IArchivableRepository<ChatMessage>
{
    
}