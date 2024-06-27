using System.Runtime.Serialization;

namespace App.Core.Exceptions;

[Serializable]
public class BadRequestException : AppException {
    public BadRequestException(string message) : base(message) {
    }

    public BadRequestException(string message, Exception inner) : base(message, inner) {
    }

    protected BadRequestException(SerializationInfo info,
                                  StreamingContext  context) : base(info, context) {
    }
}