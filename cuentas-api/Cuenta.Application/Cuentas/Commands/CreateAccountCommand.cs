using Cuenta.Application.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace Cuenta.Application.Cuentas.Commands
{
   public class CreateAccountCommand : IRequest<CreateAccountResponseModel>
   {
      [JsonPropertyName("Numero Cuenta")]
      public String? AccountId { get; set; }
      [JsonPropertyName("Tipo")]
      public String? AccountType { get; set; }
      [JsonPropertyName("Saldo Inicial")]
      public double AccountAmount { get; set; }
      [JsonPropertyName("Estado")]
      public bool AccountState { get; set; }
      [JsonPropertyName("Cliente")]
      public String? ClientName { get; set; }
   }
}
