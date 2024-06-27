using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class AccessDeniedException : AppException {
    public AccessDeniedException() : this(SecurityErrorMessages.AccessDenied) {
    }

    public AccessDeniedException(string message) : base(message) {
    }

    public AccessDeniedException(string message, Exception inner) : base(message, inner) {
    }

    protected AccessDeniedException(SerializationInfo info,
                                    StreamingContext  context) : base(info, context) {
    }
}