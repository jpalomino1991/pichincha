using AutoFixture;
using AutoMapper;
using Cliente.Application.Clientes.Commands;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Test.Moq;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Cliente.Application.Test.Clientes.Commands
{
   public class DeleteClientCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IClientRepository> _clientRepository;
      private readonly Fixture _fixture;

      public DeleteClientCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _clientRepository = new Mock<IClientRepository>();
      }

      [Fact]
      public async Task DeleteClientCommandHandler_ClientNotExists()
      {
         //Arrange
         var request = new DeleteClientCommand
         {
            Id = 1,
         };

         _clientRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<ClientEntity, bool>>>())).ReturnsAsync((ClientEntity)null);

         //Act
         var handler = new DeleteClientCommandHandler(
            _mapper,
            _clientRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task DeleteClientCommandHandler_Ok()
      {
         //Arrange
         var clientEntityMock = _fixture.Create<ClientEntity>();

         var request = new DeleteClientCommand
         {
            Id = 1,
         };

         _clientRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<ClientEntity, bool>>>())).ReturnsAsync(clientEntityMock);
         _clientRepository.Setup(u => u.UpdateAsync(clientEntityMock)).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new DeleteClientCommandHandler(
            _mapper,
            _clientRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.IsType<int>(response);
      }
   }
}
