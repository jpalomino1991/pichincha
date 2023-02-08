using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Movimiento.Application.Response
{
   public class GetMovementByDataResponseModel
   {
      [JsonPropertyName("Fecha")]
      public DateTime MovementDate { get; set; }
      [JsonPropertyName("Cliente")]
      public String? ClientName { get; set; }
      [JsonPropertyName("Cuenta")]
      public String? AccountId { get; set; }
      [JsonPropertyName("Tipo")]
      public String? MovementType { get; set; }
      [JsonPropertyName("Saldo Inicial")]
      public double MovementInitialAmount { get; set; }
      [JsonPropertyName("Estado")]
      public bool MovementState { get; set; }
      [JsonPropertyName("Movimiento")]
      public double MovementAmount { get; set; }
      [JsonPropertyName("Saldo Disponible")]
      public double MovementBalance { get; set; }
   }
}
