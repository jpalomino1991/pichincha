using AutoMapper;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Common.Services;
using Cuenta.Application.Common.Validators.Custom;
using Cuenta.Application.Response;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using MediatR;

namespace Cuenta.Application.Cuentas.Commands
{
   public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand, CreateAccountResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IAccountRepository _accountRepository;
      private readonly IClientService _clientService;
      public UpdateAccountCommandHandler(IMapper mapper, IAccountRepository accountRepository, IClientService clientService)
      {
         _mapper = mapper;
         _accountRepository = accountRepository;
         _clientService = clientService;
      }

      public async Task<CreateAccountResponseModel> Handle(UpdateAccountCommand request, CancellationToken cancellationToken)
      {
         AccountEntity account = await _accountRepository.GetAsync(a => a.AccountId.Equals(request.AccountId) && a.AccountState == true);

         var validator = new UpdateAccountValidator<AccountEntity>(account);
         var validationResult = await validator.ValidateAsync(request.AccountId, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         ClientResponseModel client = await _clientService.GetClientById(account.ClientId);

         account.ClientId = client.ClientId;
         account.AccountAmount = request.AccountAmount;
         account.AccountType = request.AccountType;

         await _accountRepository.UpdateAsync(account);

         await _accountRepository.SaveChangesAsync();

         account.ClientName = client.Person.PersonName;
         return _mapper.Map<CreateAccountResponseModel>(account);
      }
   }
}
