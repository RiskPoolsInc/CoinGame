using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class UserNotFoundException : AccessDeniedException {
    public UserNotFoundException(string email) : base(string.Format(SecurityErrorMessages.UserNotFound, email)) {
    }

    protected UserNotFoundException(SerializationInfo info,
                                    StreamingContext  context) : base(info, context) {
    }
}