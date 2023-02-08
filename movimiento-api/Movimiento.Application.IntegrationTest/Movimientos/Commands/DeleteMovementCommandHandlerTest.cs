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
using System.Linq.Expressions;
using Xunit;

namespace Movimiento.Application.Test.Movimientos.Commands
{
   public class DeleteMovementCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IMovementRepository> _movementRepository;
      private readonly Mock<IAccountService> _accountService;
      private readonly Fixture _fixture;

      public DeleteMovementCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _movementRepository = new Mock<IMovementRepository>();
         _accountService = new Mock<IAccountService>();
      }

      [Fact]
      public async Task DeleteMovementCommandHandler_NoMovement()
      {
         //Arrange
         var request = new DeleteMovementCommand
         {
            Id = 1,
         };

         _movementRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<MovementEntity, bool>>>())).ReturnsAsync((MovementEntity)null);

         //Act
         var handler = new DeleteMovementCommandHandler(
            _mapper,
            _movementRepository.Object,
            _accountService.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task DeleteMovementCommandHandler_Ok()
      {
         //Arrange
         var movementEntityMock = _fixture.Create<MovementEntity>();
         var accountEntityMock = _fixture.Create<AccountResponseModel>();

         var request = new DeleteMovementCommand
         {
            Id = 1,
         };

         _movementRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<MovementEntity, bool>>>())).ReturnsAsync(movementEntityMock);
         _movementRepository.Setup(u => u.UpdateAsync(movementEntityMock)).ReturnsAsync(movementEntityMock);
         _accountService.Setup(u => u.UpdateAccount(It.IsAny<String>(), It.IsAny<AccountDto>())).ReturnsAsync(accountEntityMock);
         _accountService.Setup(u => u.GetAccount(It.IsAny<String>())).ReturnsAsync(accountEntityMock);

         //Act
         var handler = new DeleteMovementCommandHandler(
            _mapper,
            _movementRepository.Object,
            _accountService.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.IsType<int>(response);
      }
   }
}
