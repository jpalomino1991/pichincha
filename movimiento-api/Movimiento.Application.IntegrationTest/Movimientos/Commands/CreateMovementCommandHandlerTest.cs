using AutoFixture;
using AutoMapper;
using Moq;
using Movimiento.Application.Common.Dtos;
using Movimiento.Application.Common.Exceptions;
using Movimiento.Application.Common.Services;
using Movimiento.Application.Movimientos.Commands;
using Movimiento.Application.Response;
using Movimiento.Application.Test.Moq;
using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;
using Xunit;

namespace Movimiento.Application.Test.Movimientos.Commands
{
   public class CreateMovementCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IMovementRepository> _movementRepository;
      private readonly Mock<IAccountService> _accountService;
      private readonly Fixture _fixture;

      public CreateMovementCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _movementRepository = new Mock<IMovementRepository>();
         _accountService = new Mock<IAccountService>();
      }

      [Fact]
      public async Task CreateMovementCommandHandler_NoBalance()
      {
         //Arrange
         var request = new CreateMovementCommand
         {
            AccountId = "1111",
            AccountType = "Ahorro",
            AccountAmount = 0,
            AccountState = true,
            Movement = "Retiro de 255"
         };

         //Act
         var handler = new CreateMovementCommandHandler(
            _mapper,
            _movementRepository.Object,
            _accountService.Object
         );

         //Assert
         await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task CreateMovementCommandHandler_Created()
      {
         //Arrange
         var movementEntityMock = _fixture.Create<MovementEntity>();
         var accountEntityMock = _fixture.Create<AccountResponseModel>();

         var request = new CreateMovementCommand
         {
            AccountId = "1111",
            AccountType = "Ahorro",
            AccountAmount = 2000,
            AccountState = true,
            Movement = "Retiro de 255"
         };

         _movementRepository.Setup(u => u.AddAsync(movementEntityMock)).ReturnsAsync(movementEntityMock);
         _accountService.Setup(u => u.UpdateAccount(It.IsAny<String>(), It.IsAny<AccountDto>())).ReturnsAsync(accountEntityMock);
         _accountService.Setup(u => u.GetAccount(It.IsAny<String>())).ReturnsAsync(accountEntityMock);

         //Act
         var handler = new CreateMovementCommandHandler(
            _mapper,
            _movementRepository.Object,
            _accountService.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<CreateMovementResponseModel>(response);
      }
   }
}
