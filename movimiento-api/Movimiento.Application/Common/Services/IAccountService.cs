using Movimiento.Application.Common.Dtos;
using Movimiento.Application.Response;

namespace Movimiento.Application.Common.Services
{
   public interface IAccountService
   {
      Task<AccountResponseModel> GetAccount(String? accountId);
      Task<AccountResponseModel> UpdateAccount(String? accountId, AccountDto account);
   }
}
