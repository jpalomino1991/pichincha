using AutoMapper;
using MediatR;
using Movimiento.Application.Common.Dtos;
using Movimiento.Application.Common.Exceptions;
using Movimiento.Application.Common.Services;
using Movimiento.Application.Common.Validators.Custom;
using Movimiento.Application.Response;
using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;

namespace Movimiento.Application.Movimientos.Commands
{
   public class CreateMovementCommandHandler : IRequestHandler<CreateMovementCommand, CreateMovementResponseModel>
   {
      private readonly IMapper _mapper;
      private readonly IMovementRepository _movementRepository;
      private readonly IAccountService _accountService;
      public CreateMovementCommandHandler(IMapper mapper, IMovementRepository movementRepository, IAccountService accountService)
      {
         _mapper = mapper;
         _movementRepository = movementRepository;
         _accountService = accountService;
      }

      public async Task<CreateMovementResponseModel> Handle(CreateMovementCommand request, CancellationToken cancellationToken)
      {
         MovementEntity movement = _mapper.Map<MovementEntity>(request);

         movement.MovementAmount = getBalance(request.Movement);
         movement.MovementDate = DateTime.UtcNow;

         var validator = new CreateMovementValidator(request);
         var validationResult = await validator.ValidateAsync(movement, cancellationToken);

         if (validationResult.IsValid == false) throw new ValidationException(validationResult);

         double currentAmount = request.AccountAmount + movement.MovementAmount;
         movement.MovementBalance = currentAmount;

         var updateAccount = new AccountDto { 
            AccountAmount = currentAmount,
            AccountType= request.AccountType
         };

         await _accountService.UpdateAccount(request.AccountId, updateAccount);

         await _movementRepository.AddAsync(movement);

         await _movementRepository.SaveChangesAsync();
         return _mapper.Map<CreateMovementResponseModel>(movement);
      }

      public double getBalance(string balanceString)
      {
         double sign = 1;
         if (balanceString.Contains("Retiro"))
            sign = -1;
         return double.Parse(balanceString.Trim().Split(' ')[2]) * sign;
      }
   }
}
