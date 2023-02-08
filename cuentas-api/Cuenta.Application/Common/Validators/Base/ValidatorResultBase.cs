using FluentValidation.Results;

namespace Cuenta.Application.Common.Validators.Base
{
    public class ValidatorResultBase<T> : ValidationResult
    {
        public T? Body { get; set; }
    }
}