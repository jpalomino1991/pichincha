using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuenta.Application.Common.Validators.Custom
{
   public class CreateAccountValidator : AbstractValidator<AccountEntity>
   {
      public CreateAccountValidator(IAccountRepository accountRepository)
      {
         RuleFor(c => c)
            .MustAsync(async (c, _) =>
               !await accountRepository.AccountExists(c.AccountId))
            .WithMessage("Account already exists");
      }
   }
}
