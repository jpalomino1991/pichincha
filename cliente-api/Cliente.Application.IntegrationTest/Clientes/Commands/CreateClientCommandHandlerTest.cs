using AutoFixture;
using AutoMapper;
using Cliente.Application.Common.Exceptions;
using Cliente.Application.Cuentas.Commands;
using Cliente.Application.Response;
using Cliente.Application.Test.Moq;
using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using Moq;
using Xunit;

namespace Cliente.Application.Test.Clientes.Commands
{
   public class CreateClientCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IClientRepository> _clientRepository;
      private readonly Mock<IPersonRepository> _personRepository;
      private readonly Fixture _fixture;

      public CreateClientCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _clientRepository = new Mock<IClientRepository>();
         _personRepository = new Mock<IPersonRepository>();
      }

      [Fact]
      public async Task CreateClientCommandHandler_UserExists()
      {
         //Arrange
         var request = new CreateClientCommand {
            PersonName = "Jhon Perez",
            PersonDirection = "Otavalo sn y principal",
            PersonPhone = "98713245",
            ClientPassword = "1234",
            ClientState = true
         };

         _personRepository.Setup(u => u.ClientExists(It.IsAny<PersonEntity>())).ReturnsAsync(true);

         //Act
         var handler = new CreateClientCommandHandler(
            _mapper,
            _clientRepository.Object,
            _personRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task CreateClientCommandHandler_Created()
      {
         //Arrange
         var clientEntityMock = _fixture.Create<ClientEntity>();

         var request = new CreateClientCommand
         {
            PersonName = "Jhon Perez",
            PersonDirection = "Otavalo sn y principal",
            PersonPhone = "98713245",
            ClientPassword = "1234",
            ClientState = true
         };

         _personRepository.Setup(u => u.ClientExists(It.IsAny<PersonEntity>())).ReturnsAsync(false);
         _clientRepository.Setup(u => u.AddAsync(clientEntityMock)).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new CreateClientCommandHandler(
            _mapper,
            _clientRepository.Object,
            _personRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<CreateClientResponseModel>(response);
      }
   }
}
