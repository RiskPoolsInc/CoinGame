using MediatR;
using Newtonsoft.Json.Linq;

namespace App.Core.Messages.Users {

public class UserChangedMessage : SyncUserMessage, IRequest<bool>
{
}
}