using FluentValidation;
using Movimiento.Application.Movimientos.Commands;
using Movimiento.Application.Response;
using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;

namespace Movimiento.Application.Common.Validators.Custom
{
   public class CreateMovementValidator : AbstractValidator<MovementEntity>
   {
      public CreateMovementValidator(CreateMovementCommand account)
      {
         RuleFor(x => x)
            .Must((c,_) => c.MovementBalance > 0 || c.MovementAmount > 0)
            .WithMessage("Saldo no disponible");
      }
   }
}
