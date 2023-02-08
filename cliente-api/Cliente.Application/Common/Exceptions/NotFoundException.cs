﻿using FluentValidation.Results;

namespace Cliente.Application.Common.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public List<string> Errors { get; set; } = new();
        public NotFoundException(ValidationResult validationResult)
        {
            var error = validationResult.Errors.FirstOrDefault(x => !string.IsNullOrEmpty(x.ErrorMessage))?.ErrorMessage;
            if (string.IsNullOrEmpty(error)) return;
            Errors.Add(error);
        }

    }
}