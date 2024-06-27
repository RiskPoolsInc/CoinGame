using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class AppException : Exception {
    public AppException() : base(SecurityErrorMessages.UnknownError) {
    }

    public AppException(string message) : base(message) {
    }

    public AppException(string message, Exception inner) : base(message, inner) {
    }

    protected AppException(SerializationInfo info,
                           StreamingContext  context) : base(info, context) {
    }
}