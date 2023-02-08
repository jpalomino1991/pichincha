using AutoMapper;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Common.Services;
using Cuenta.Application.Common.Validators.Custom;
using Cuenta.Application.Response;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using MediatR;

namespace Cuenta.Application.Cuentas.Queries
{
   public class GetAccountByIdQueryHandler : IRequestHandler<GetAccountByIdQuery, GetAccountResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IAccountRepository _accountRepository;
      private readonly IClientService _clientService;
      public GetAccountByIdQueryHandler(IMapper mapper, IAccountRepository accountRepository, IClientService clientService)
      {
         _mapper = mapper;
         _accountRepository = accountRepository;
         _clientService = clientService;
      }

      public async Task<GetAccountResponseModel> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
      {
         AccountEntity account = await _accountRepository.GetAsync(a => a.AccountId.Equals(request.AccountId) && a.AccountState == true);

         var validator = new UpdateAccountValidator<AccountEntity>(account);
         var validationResult = await validator.ValidateAsync(request.AccountId, cancellationToken);

         ClientResponseModel client = await _clientService.GetClientById(account.ClientId);

         account.ClientName = client.Person.PersonName;

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         return _mapper.Map<GetAccountResponseModel>(account);
      }
   }
}
