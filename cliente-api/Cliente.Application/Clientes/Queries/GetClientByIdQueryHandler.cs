using AutoMapper;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Common.Validators.Custom;
using Cliente.Application.Response;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using MediatR;

namespace Cliente.Application.Clientes.Queries
{
   public class GetClientByIdQueryHandler : IRequestHandler<GetClientByIdQuery, GetClientResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IClientRepository _clientRepository;
      public GetClientByIdQueryHandler(IMapper mapper, IClientRepository clientRepository)
      {
         _mapper = mapper;
         _clientRepository = clientRepository;
      }

      public async Task<GetClientResponseModel> Handle(GetClientByIdQuery request, CancellationToken cancellationToken)
      {
         ClientEntity client = await _clientRepository.GetClientById(request.Id);

         var validator = new GetClientByIdValidator<ClientEntity>(client);
         var validationResult = await validator.ValidateAsync(request.Id, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         return _mapper.Map<GetClientResponseModel>(client);
      }
   }
}
