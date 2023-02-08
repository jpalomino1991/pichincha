using AutoMapper;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Common.Validators.Custom;
using Cliente.Application.Cuentas.Commands;
using Cliente.Application.Response;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cliente.Application.Clientes.Commands
{
   public class UpdateClientCommandHandler : IRequestHandler<UpdateClientCommand, CreateClientResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IClientRepository _clientRepository;
      public UpdateClientCommandHandler(IMapper mapper, IClientRepository clientRepository)
      {
         _mapper = mapper;
         _clientRepository = clientRepository;

         _clientRepository.SetProperties(new()
            {
                property => property.Person
            });
      }
      public async Task<CreateClientResponseModel> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
      {
         ClientEntity client = await _clientRepository.GetAsync(c => c.ClientId == request.Id);

         var validator = new UpdateClientValidator<ClientEntity>(client);
         var validationResult = await validator.ValidateAsync(request.Id, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         client.Person.PersonDirection = request.PersonDirection;
         client.Person.PersonPhone = request.PersonPhone;
         client.Person.PersonName = request.PersonName;

         await _clientRepository.UpdateAsync(client);

         await _clientRepository.SaveChangesAsync();
         return _mapper.Map<CreateClientResponseModel>(client);
      }
   }
}
