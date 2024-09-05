using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class NotFoundException : AppException {
    public NotFoundException() : this(SecurityErrorMessages.NotFound) {
    }

    public NotFoundException(string message) : base(message) {
    }

    public NotFoundException(string message, Exception inner) : base(message, inner) {
    }

    protected NotFoundException(SerializationInfo info,
                                StreamingContext  context) : base(info, context) {
    }
}