using FluentValidation.Results;

namespace Cliente.Application.Common.Validators.Base
{
    public class ValidatorResultBase<T> : ValidationResult
    {
        public T? Body { get; set; }
    }
}