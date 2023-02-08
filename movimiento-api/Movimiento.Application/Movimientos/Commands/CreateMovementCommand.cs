using MediatR;
using Movimiento.Application.Response;
using System.Text.Json.Serialization;

namespace Movimiento.Application.Movimientos.Commands
{
   public class CreateMovementCommand : IRequest<CreateMovementResponseModel>
   {
      [JsonPropertyName("Numero Cuenta")]
      public String? AccountId { get; set; }
      [JsonPropertyName("Tipo")]
      public String? AccountType { get; set; }
      [JsonPropertyName("Saldo Inicial")]
      public double AccountAmount { get; set; }
      [JsonPropertyName("Estado")]
      public bool AccountState { get; set; }
      [JsonPropertyName("Movimiento")]
      public String? Movement { get; set; }
   }
}
