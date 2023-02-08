using Cuenta.Application.Response;
using MediatR;

namespace Cuenta.Application.Cuentas.Queries
{
   public class GetAccountByIdQuery : IRequest<GetAccountResponseModel>
   {
      public String? AccountId { get; set; }

      public GetAccountByIdQuery(String? accountId)
      {
         AccountId = accountId;
      }
   }
}
