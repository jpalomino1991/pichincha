using System.Text.Json.Serialization;

namespace Cliente.Application.Response
{
   public class CreateClientResponseModel
   {
      [JsonPropertyName("Id")]
      public int ClientId { get; set; }
      [JsonPropertyName("Nombres")]
      public String? PersonName { get;set; }
      [JsonPropertyName("Direccion")]
      public String? PersonDirection { get; set; }
      [JsonPropertyName("Telefono")]
      public String? PersonPhone { get; set; }
      [JsonPropertyName("Estado")]
      public bool ClientState { get; set; }
   }
}
