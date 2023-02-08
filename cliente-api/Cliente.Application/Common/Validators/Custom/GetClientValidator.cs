using Cliente.Application.Common.Validators.Base;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Common.Validators.Custom
{
   public class GetClientValidator<TBody> : AbstractValidatorBase<TBody?, String?>
   {
      public GetClientValidator(TBody? entity) : base(entity)
      {
         RuleFor(c => c)
            .Must((id, _) => entity != null || String.IsNullOrEmpty(id))
            .WithMessage("Client doesn't exists");
      }
   }
}
