using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class SecurityCodeNotFoundException : NotFoundException {
    public SecurityCodeNotFoundException(string code) : base(string.Format(SecurityErrorMessages.UnknownSecurityCode, code)) {
    }

    protected SecurityCodeNotFoundException(SerializationInfo info,
                                            StreamingContext  context) : base(info, context) {
    }
}