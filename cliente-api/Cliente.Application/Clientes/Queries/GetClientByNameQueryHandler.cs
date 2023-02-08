using AutoMapper;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Common.Validators.Custom;
using Cliente.Application.Response;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Clientes.Queries
{
   public class GetClientByNameQueryHandler : IRequestHandler<GetClientByNameQuery, GetClientResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IClientRepository _clientRepository;
      public GetClientByNameQueryHandler(IMapper mapper, IClientRepository clientRepository)
      {
         _mapper= mapper;
         _clientRepository= clientRepository;
      }

      public async Task<GetClientResponseModel> Handle(GetClientByNameQuery request, CancellationToken cancellationToken)
      {
         ClientEntity client = await _clientRepository.GetClientByName(request.Name);

         var validator = new GetClientValidator<ClientEntity>(client);
         var validationResult = await validator.ValidateAsync(request.Name, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         return _mapper.Map<GetClientResponseModel>(client);
      }
   }
}
