using System.Text.Json.Serialization;

namespace Movimiento.Application.Common.Dtos
{
   public class AccountDto
   {
      [JsonPropertyName("Tipo")]
      public String? AccountType { get; set; }
      [JsonPropertyName("Saldo Inicial")]
      public double AccountAmount { get; set; }
      [JsonPropertyName("Cliente")]
      public String? ClientName { get; set; }
   }
}
