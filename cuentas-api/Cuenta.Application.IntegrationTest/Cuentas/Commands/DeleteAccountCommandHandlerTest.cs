using AutoFixture;
using AutoMapper;
using Cuenta.Application.Common.Exceptions;
using Cuenta.Application.Cuentas.Commands;
using Cuenta.Application.Test.Moq;
using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using Moq;
using System.Linq.Expressions;
using Xunit;

namespace Cuenta.Application.Test.Cuentas.Commands
{
   public class DeleteAccountCommandHandlerTest
   {
      private readonly IMapper _mapper;
      private readonly Mock<IAccountRepository> _accountRepository;
      private readonly Fixture _fixture;

      public DeleteAccountCommandHandlerTest()
      {
         _fixture = new Fixture();
         _fixture.Behaviors.Remove(new ThrowingRecursionBehavior());
         _fixture.Behaviors.Add(new OmitOnRecursionBehavior());
         _mapper = MapperSetup.InitMappings();
         _accountRepository = new Mock<IAccountRepository>();
      }

      [Fact]
      public async Task DeleteAccountCommandHandler_ClientNotExists()
      {
         //Arrange
         var request = new DeleteAccountCommand
         {
            Id = "1111",
         };

         _accountRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>())).ReturnsAsync((AccountEntity)null);

         //Act
         var handler = new DeleteAccountCommandHandler(
            _mapper,
            _accountRepository.Object
         );

         //Assert
         await Assert.ThrowsAsync<NotFoundException>(() => handler.Handle(request, CancellationToken.None));
      }

      [Fact]
      public async Task DeleteAccountCommandHandler_Ok()
      {
         //Arrange
         var accountEntityMock = _fixture.Create<AccountEntity>();

         var request = new DeleteAccountCommand
         {
            Id = "1111",
         };

         _accountRepository.Setup(u => u.GetAsync(It.IsAny<Expression<Func<AccountEntity, bool>>>())).ReturnsAsync(accountEntityMock);
         _accountRepository.Setup(u => u.UpdateAsync(accountEntityMock)).ReturnsAsync(accountEntityMock);

         //Act
         var handler = new DeleteAccountCommandHandler(
            _mapper,
            _accountRepository.Object
         );

         var response = await handler.Handle(request, CancellationToken.None);

         //Assert
         Assert.IsType<String>(response);
      }
   }
}
