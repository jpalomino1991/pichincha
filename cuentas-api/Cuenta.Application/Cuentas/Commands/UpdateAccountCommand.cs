using Cuenta.Application.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace Cuenta.Application.Cuentas.Commands
{
   public class UpdateAccountCommand : IRequest<CreateAccountResponseModel>
   {
      [JsonIgnore]
      public String? AccountId { get; set; }
      [JsonPropertyName("Tipo")]
      public String? AccountType { get; set; }
      [JsonPropertyName("Saldo Inicial")]
      public double AccountAmount { get; set; }
   }
}
