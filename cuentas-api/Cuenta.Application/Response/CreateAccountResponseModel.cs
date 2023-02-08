using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cuenta.Application.Response
{
   public class CreateAccountResponseModel
   {
      public String? AccountId { get; set; }
      [JsonPropertyName("Tipo")]
      public String? AccountType { get; set; }
      [JsonPropertyName("Saldo Inicial")]
      public double AccountAmount { get; set; }
      [JsonPropertyName("Estado")]
      public bool AccountState { get; set; }
      [JsonPropertyName("Cliente Id")]
      public int ClientId { get; set; }
      [JsonPropertyName("Nombres")]
      public String? ClientName { get; set; }
   }
}
