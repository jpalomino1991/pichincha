using AutoFixture;
using AutoMapper;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Common.Services;
using Cuenta.Application.Cuentas.Commands;
using Cuenta.Application.Response;
using Cuenta.Application.Test.Moq;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using Moq;
using Xunit;

namespace Cuenta.Application.Test.Cuentas.Commands
{
   public class CreateAccountCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IAccountRepository> _accountRepository;
      private readonly Mock<IClientService> _clientService;
      private readonly Fixture _fixture;

      public CreateAccountCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _accountRepository = new Mock<IAccountRepository>();
         _clientService = new Mock<IClientService>();
      }

      [Fact]
      public async Task CreateAccountCommandHandler_AccountExists()
      {
         //Arrange
         var request = new CreateAccountCommand
         {
            AccountId = "1111",
            AccountType = "Ahorro",
            AccountAmount = 2000,
            AccountState = true,
            ClientName = "Juan Perez"
         };

         _accountRepository.Setup(u => u.AccountExists(It.IsAny<String>())).ReturnsAsync(true);

         //Act
         var handler = new CreateAccountCommandHandler(
            _mapper,
            _accountRepository.Object,
            _clientService.Object
         );

         //Assert
         await Assert.ThrowsAsync<ValidationException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task CreateAccountCommandHandler_Created()
      {
         //Arrange
         var accountEntityMock = _fixture.Create<AccountEntity>();
         var clientEntityMock = _fixture.Create<ClientResponseModel>();

         var request = new CreateAccountCommand
         {
            AccountId = "1111",
            AccountType = "Ahorro",
            AccountAmount = 2000,
            AccountState = true,
            ClientName = "Juan Perez"
         };

         _accountRepository.Setup(u => u.AccountExists(It.IsAny<String>())).ReturnsAsync(false);
         _accountRepository.Setup(u => u.AddAsync(accountEntityMock)).ReturnsAsync(accountEntityMock);
         _clientService.Setup(u => u.GetClientByName(It.IsAny<String>())).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new CreateAccountCommandHandler(
            _mapper,
            _accountRepository.Object,
            _clientService.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<CreateAccountResponseModel>(response);
      }
   }
}
