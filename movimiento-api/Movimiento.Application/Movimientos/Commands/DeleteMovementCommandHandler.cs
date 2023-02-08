using AutoMapper;
using MediatR;
using Movimiento.Application.Common.Dtos;
using Movimiento.Application.Common.Exceptions;
using Movimiento.Application.Common.Services;
using Movimiento.Application.Common.Validators.Custom;
using Movimiento.Application.Response;
using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;
using System.Security.Principal;

namespace Movimiento.Application.Movimientos.Commands
{
   public class DeleteMovementCommandHandler : IRequestHandler<DeleteMovementCommand, int>
   {
      private readonly IMapper _mapper;
      private readonly IMovementRepository _movementRepository;
      private readonly IAccountService _accountService;
      public DeleteMovementCommandHandler(IMapper mapper, IMovementRepository movementRepository, IAccountService accountService)
      {
         _mapper = mapper;
         _movementRepository = movementRepository;
         _accountService = accountService;
      }

      public async Task<int> Handle(DeleteMovementCommand request, CancellationToken cancellationToken)
      {
         MovementEntity movement = await _movementRepository.GetAsync(c => c.MovementId == request.Id && c.MovementState == true);

         var validator = new DeleteMovementValidator<MovementEntity>(movement);
         var validationResult = await validator.ValidateAsync(request.Id, cancellationToken);

         if (validationResult.IsValid == false) throw new NotFoundException(validationResult);

         AccountResponseModel account = await _accountService.GetAccount(movement.AccountId);

         var updateAccount = new AccountDto
         {
            AccountAmount = account.AccountAmount - movement.MovementAmount,
            AccountType = movement.MovementType
         };

         await _accountService.UpdateAccount(movement.AccountId, updateAccount);

         movement.MovementState = false;

         await _movementRepository.UpdateAsync(movement);

         await _movementRepository.SaveChangesAsync();
         return movement.MovementId;
      }
   }
}
