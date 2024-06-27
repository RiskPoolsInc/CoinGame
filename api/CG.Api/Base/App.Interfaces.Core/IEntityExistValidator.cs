using FluentValidation;

namespace App.Interfaces.Core;

public interface IEntityExistValidator<T> : IValidator<T> {
}