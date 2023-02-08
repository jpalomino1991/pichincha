using AutoMapper;
using MediatR;
using Movimiento.Application.Common.Services;
using Movimiento.Application.Response;
using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;

namespace Movimiento.Application.Movimientos.Queries
{
   public class GetMovementByDateQueryHandler : IRequestHandler<GetMovementByDateQuery, List<GetMovementByDataResponseModel>>
   {
      private readonly IMapper _mapper;
      private readonly IMovementRepository _movementRepository;
      private readonly IClientService _clientService;
      public GetMovementByDateQueryHandler(IMapper mapper, IMovementRepository movementRepository, IClientService clientService)
      {
         _mapper = mapper;
         _movementRepository = movementRepository;
         _clientService = clientService;
      }

      public async Task<List<GetMovementByDataResponseModel>> Handle(GetMovementByDateQuery request, CancellationToken cancellationToken)
      {
         List<MovementEntity> listMovement = await _movementRepository.ListAsync(m => m.MovementDate > request.BeginDate && m.MovementDate < request.EndDate);

         ClientResponseModel client = await _clientService.GetClient(request.ClientId);

         foreach (MovementEntity entity in listMovement)
         {
            entity.ClientName = client.Person.PersonName;
         }

         return _mapper.Map<List<GetMovementByDataResponseModel>>(listMovement);
      }
   }
}
