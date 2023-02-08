using AutoFixture;
using AutoMapper;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Common.Services;
using Cuenta.Application.Cuentas.Queries;
using Cuenta.Application.Response;
using Cuenta.Application.Test.Moq;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Cuenta.Application.Test.Cuentas.Queries
{
   public class GetAccountByIdQueryHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IAccountRepository> _accountRepository;
      private readonly Mock<IClientService> _clientService;
      private readonly Fixture _fixture;

      public GetAccountByIdQueryHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _accountRepository = new Mock<IAccountRepository>();
         _clientService = new Mock<IClientService>();
      }

      [Fact]
      public async Task GetAccountByIdQueryHandler_ClientNotExists()
      {
         //Arrange
         var name = "name";
         var request = new GetAccountByIdQuery(name);

         _accountRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>())).ReturnsAsync((AccountEntity)null);

         //Act
         var handler = new GetAccountByIdQueryHandler(
            _mapper,
            _accountRepository.Object,
            _clientService.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task GetAccountByIdQueryHandler_Ok()
      {
         //Arrange
         var accountEntityMock = _fixture.Create<AccountEntity>();
         var clientEntityMock = _fixture.Create<ClientResponseModel>();

         var request = new GetAccountByIdQuery("name");

         _accountRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>())).ReturnsAsync(accountEntityMock);
         _clientService.Setup(u => u.GetClientById(It.IsAny<int>())).ReturnsAsync(clientEntityMock);

         //Act
         var handler = new GetAccountByIdQueryHandler(
            _mapper,
            _accountRepository.Object,
            _clientService.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.NotNull(response);
         Assert.IsType<GetAccountResponseModel>(response);
      }
   }
}
