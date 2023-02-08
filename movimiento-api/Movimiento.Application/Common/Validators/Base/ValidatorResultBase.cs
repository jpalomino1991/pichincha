using FluentValidation.Results;

namespace Movimiento.Application.Common.Validators.Base
{
    public class ValidatorResultBase<T> : ValidationResult
    {
        public T? Body { get; set; }
    }
}