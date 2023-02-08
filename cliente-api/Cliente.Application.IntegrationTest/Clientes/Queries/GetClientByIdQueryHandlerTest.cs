using AutoFixture;
using AutoMapper;
using Cliente.Application.Clientes.Queries;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Response;
using Cliente.Application.Test.Moq;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using Moq;
using Xunit;

namespace Cliente.Application.Test.Clientes.Queries
{
   public class GetClientByIdQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IClientRepository> _clientRepository;
      private readonly Fixture _fixture;

      public GetClientByIdQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _clientRepository = new Mock<IClientRepository>();
      }

      [Fact]
      public async Task GetClientByIdQueryHandler_ClientNotExists()
      {
         //Arrange
         var id = 1;
         var request = new GetClientByIdQuery(id);

         _clientRepository.Setup(u => u.GetClientById(It.IsAny<int>())).ReturnsAsync((ClientEntity)null);

         //Act
         var handler = new GetClientByIdQueryHandler(
            _mapper,
            _clientRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task GetClientByIdQueryHandler_Ok()
      {
         //Arrange
         var clientEntityMock = _fixture.Create<ClientEntity>();

         var request = new GetClientByIdQuery(1);

         _clientRepository.Setup(u => u.GetClientById(It.IsAny<int>())).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new GetClientByIdQueryHandler(
            _mapper,
            _clientRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<GetClientResponseModel>(response);
      }
   }
}
