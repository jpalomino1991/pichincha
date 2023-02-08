using AutoMapper;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Common.Validators.Custom;
using Cliente.Application.Response;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using MediatR;

namespace Cliente.Application.Cuentas.Commands
{
   public class CreateClientCommandHandler : IRequestHandler<CreateClientCommand, CreateClientResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IClientRepository _clientRepository;
      private readonly IPersonRepository _personRepository;

      public CreateClientCommandHandler(IMapper mapper, IClientRepository clientRepository, IPersonRepository personRepository)
      {
         _mapper = mapper;
         _clientRepository = clientRepository;
         _personRepository = personRepository;
      }

      public async Task<CreateClientResponseModel> Handle(CreateClientCommand request, CancellationToken cancellationToken)
      {
         PersonEntity person = _mapper.Map<PersonEntity>(request);

         var validator = new CreateClientValidator(_personRepository);
         var validationResult = await validator.ValidateAsync(person);

         if (validationResult.IsValid == false) throw new ValidationException(validationResult);

         ClientEntity client = _mapper.Map<ClientEntity>(request);

         client.Person = person;
         await _clientRepository.AddAsync(client);

         await _clientRepository.SaveChangesAsync();
         return _mapper.Map<CreateClientResponseModel>(client);
      }
   }
}
