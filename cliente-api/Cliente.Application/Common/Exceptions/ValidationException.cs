using System.Net;
using FluentValidation.Results;

namespace Cliente.Application.Common.Exceptions
{
    public class ValidationException : ApplicationException
    {
        private const string DefaultErrorCode = "PredicateValidator";
        private readonly string _customErrorCode = $"{(int)HttpStatusCode.BadRequest}";

        public List<(string, string)> Errors { get; set; } = new();

        public ValidationException(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                var errorCode = error.ErrorCode == DefaultErrorCode ? _customErrorCode : error.ErrorCode;

                Errors.Add((error.ErrorMessage, errorCode));
            }
        }
        public ValidationException(string errorMessage, int errorCode)
        {
            Errors.Add((errorMessage, errorCode.ToString()));
        }
    }
}