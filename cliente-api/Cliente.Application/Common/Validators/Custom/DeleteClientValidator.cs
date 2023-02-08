﻿using Cliente.Application.Common.Validators.Base;
using FluentValidation;

namespace Cliente.Application.Common.Validators.Custom
{
   public class DeleteClientValidator<TBody> : AbstractValidatorBase<TBody?, int>
   {
      public DeleteClientValidator(TBody? entity) : base(entity)
      {
         RuleFor(c => c)
            .Must((id, _) => entity != null && id >= 0)
            .WithMessage("Client doesn't exists");
      }
   }
}
