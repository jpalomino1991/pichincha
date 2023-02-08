using Cuenta.Application.Common.Validators.Base;
using FluentValidation;

namespace Cuenta.Application.Common.Validators.Custom
{
    public class UpdateAccountValidator<TBody> : AbstractValidatorBase<TBody?, string?>
    {
        public UpdateAccountValidator(TBody? entity) : base(entity)
        {
            RuleFor(c => c)
               .Must((id, _) => entity != null || string.IsNullOrEmpty(id))
               .WithMessage("Account doesn't exists or is deleted");
        }
    }
}
