using FluentValidation;
using Cliente.Application.Common.Validators.Base;

namespace Cliente.Application.Common.Validators.Custom
{
   public class UpdateClientValidator<TBody> : AbstractValidatorBase<TBody?, int>
   {
      public UpdateClientValidator(TBody? entity) : base(entity)
      {
         RuleFor(c => c)
            .Must((id, _) => entity != null && id >= 0)
            .WithMessage("Client doesn't exists");
      }
   }
}
