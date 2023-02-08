using AutoFixture;
using AutoMapper;
using Moq;
using Movimiento.Application.Common.Exceptions;
using Movimiento.Application.Common.Services;
using Movimiento.Application.Movimientos.Queries;
using Movimiento.Application.Response;
using Movimiento.Application.Test.Moq;
using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;
using System.Linq.Expressions;
using Xunit;

namespace Movimiento.Application.Test.Movimientos.Queries
{
   public class GetMovementByDateQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IMovementRepository> _movementRepository;
      private readonly Mock<IClientService> _clientService;
      private readonly Fixture _fixture;

      public GetMovementByDateQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _movementRepository = new Mock<IMovementRepository>();
         _clientService = new Mock<IClientService>();
      }

      [Fact]
      public async Task GetMovementByDateQueryHandler_Empty()
      {
         //Arrange
         var request = new GetMovementByDateQuery 
         {
            ClientId = 1,
            BeginDate= DateTime.Now,
            EndDate= DateTime.Now,
         };

         _movementRepository.Setup(u => u.ListAsync(It.IsAny<Expression<Func<MovementEntity, bool>>>(), null, null)).ReturnsAsync(new List<MovementEntity>());

         //Act
         var handler = new GetMovementByDateQueryHandler(
            _mapper,
            _movementRepository.Object,
            _clientService.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.IsType<List<GetMovementByDataResponseModel>>(response);
         Assert.Empty(response);
      }

      [Fact]
      public async Task GetMovementByDateQueryHandler_Ok()
      {
         //Arrange
         var movementListEntityMock = _fixture.CreateMany<MovementEntity>().ToList();
         var clientEntityMock = _fixture.Create<ClientResponseModel>();

         var request = new GetMovementByDateQuery
         {
            ClientId = 1,
            BeginDate = DateTime.Now,
            EndDate = DateTime.Now,
         };

         _movementRepository.Setup(u => u.ListAsync(It.IsAny<Expression<Func<MovementEntity, bool>>>(), null, null)).ReturnsAsync(movementListEntityMock);
         _clientService.Setup(u => u.GetClient(It.IsAny<int>())).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new GetMovementByDateQueryHandler(
            _mapper,
            _movementRepository.Object,
            _clientService.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<List<GetMovementByDataResponseModel>>(response);
         Assert.Equal(movementListEntityMock.Count, response.Count);
      }
   }
}
