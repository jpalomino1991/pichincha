using AutoFixture;
using AutoMapper;
using Cliente.Application.Clientes.Commands;
using Cliente.Application.Clientes.Queries;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Response;
using Cliente.Application.Test.Moq;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Cliente.Application.Test.Clientes.Queries
{
   public class GetClientByNameQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IClientRepository> _clientRepository;
      private readonly Fixture _fixture;

      public GetClientByNameQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _clientRepository = new Mock<IClientRepository>();
      }

      [Fact]
      public async Task GetClientByNameQueryHandler_ClientNotExists()
      {
         //Arrange
         var name = "name";
         var request = new GetClientByNameQuery(name);

         _clientRepository.Setup(u => u.GetClientByName(It.IsAny<String>())).ReturnsAsync((ClientEntity)null);

         //Act
         var handler = new GetClientByNameQueryHandler(
            _mapper,
            _clientRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task GetClientByNameQueryHandler_Ok()
      {
         //Arrange
         var clientEntityMock = _fixture.Create<ClientEntity>();

         var request = new GetClientByNameQuery("name");

         _clientRepository.Setup(u => u.GetClientByName(It.IsAny<String>())).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new GetClientByNameQueryHandler(
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
