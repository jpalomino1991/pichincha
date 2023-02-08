using FluentValidation;
using Movimiento.Application.Common.Validators.Base;

namespace Movimiento.Application.Common.Validators.Custom
{
   public class DeleteMovementValidator<TBody> : AbstractValidatorBase<TBody?, int>
   {
      public DeleteMovementValidator(TBody? entity) : base(entity)
      {
         RuleFor(c => c)
            .Must((id, _) => entity != null || id <= 0)
            .WithMessage("Movement doesn't exists or is deleted");
      }
   }
}
