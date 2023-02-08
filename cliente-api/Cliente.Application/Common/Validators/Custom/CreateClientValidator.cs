using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using FluentValidation;

namespace Cliente.Application.Common.Validators.Custom
{
   public class CreateClientValidator : AbstractValidator<PersonEntity>
   {
      public CreateClientValidator(IPersonRepository personRepository)
      {
         RuleFor(c => c)
            .MustAsync(async (c, _) => 
               !await personRepository.ClientExists(c))
            .WithMessage("Client name or phone already exists");
      }
   }
}
