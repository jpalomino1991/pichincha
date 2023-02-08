using AutoMapper;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Common.Validators.Custom;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using MediatR;

namespace Cuenta.Application.Cuentas.Commands
{
   public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand, String?>
   {
      private readonly IMapper _mapper;
      private readonly IAccountRepository _accountRepository;
      public DeleteAccountCommandHandler(IMapper mapper, IAccountRepository accountRepository)
      {
         _mapper = mapper;
         _accountRepository = accountRepository;
      }
      public async Task<String?> Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
      {
         AccountEntity account = await _accountRepository.GetAsync(c => c.AccountId.Equals(request.Id));

         var validator = new UpdateAccountValidator<AccountEntity>(account);
         var validationResult = await validator.ValidateAsync(request.Id, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         account.AccountState = false;

         await _accountRepository.UpdateAsync(account);

         await _accountRepository.SaveChangesAsync();
         return account.AccountId;
      }
   }
}
