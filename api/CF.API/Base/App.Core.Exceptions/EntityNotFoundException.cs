using System.Runtime.Serialization;
using App.Resources;

namespace App.Core.Exceptions;

[Serializable]
public class EntityNotFoundException : NotFoundException {
    public EntityNotFoundException(string entityName, int id) : base(string.Format(SecurityErrorMessages.EntityNotFound, entityName, id)) {
    }

    public EntityNotFoundException(string entityName, Guid id) : base(string.Format(SecurityErrorMessages.EntityNotFound, entityName, id)) {
    }

    public EntityNotFoundException(IEnumerable<string> validationErrors) : base(string.Join(Environment.NewLine, validationErrors)) {
    }

    protected EntityNotFoundException(SerializationInfo info,
                                      StreamingContext  context) : base(info, context) {
    }
}