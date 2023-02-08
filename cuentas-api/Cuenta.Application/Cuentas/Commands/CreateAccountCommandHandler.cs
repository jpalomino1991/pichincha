using AutoMapper;
using Cuenta.Application.Response;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using MediatR;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Common.Validators.Custom;
using Cuenta.Application.Common.Services;

namespace Cuenta.Application.Cuentas.Commands
{
   public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, CreateAccountResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IAccountRepository _accountRepository;
      private readonly IClientService _clientService;
      public CreateAccountCommandHandler(IMapper mapper, IAccountRepository accountRepository, IClientService clientService)
      {
         _mapper = mapper;
         _accountRepository = accountRepository;
         _clientService = clientService;
      }

      public async Task<CreateAccountResponseModel> Handle(CreateAccountCommand request, CancellationToken cancellationToken)
      {
         AccountEntity account = _mapper.Map<AccountEntity>(request);

         var validator = new CreateAccountValidator(_accountRepository);
         var validationResult = await validator.ValidateAsync(account, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         ClientResponseModel client = await _clientService.GetClientByName(request.ClientName);

         account.ClientId = client.ClientId;

         await _accountRepository.AddAsync(account);

         await _accountRepository.SaveChangesAsync();
         account.ClientName = client.Person.PersonName;
         return _mapper.Map<CreateAccountResponseModel>(account);
      }
   }
}
