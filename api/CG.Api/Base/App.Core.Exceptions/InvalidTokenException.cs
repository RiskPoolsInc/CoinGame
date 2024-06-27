using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class InvalidTokenException : BadRequestException {
    public InvalidTokenException() : base(SecurityErrorMessages.InvalidToken) {
    }

    protected InvalidTokenException(SerializationInfo info,
                                    StreamingContext  context) : base(info, context) {
    }
}