using Cliente.Application.Response;
using MediatR;
using System.Text.Json.Serialization;

namespace Cliente.Application.Cuentas.Commands
{
   public class CreateClientCommand : IRequest<CreateClientResponseModel>
   {
      [JsonPropertyName("Nombres")]
      public String? PersonName { get; set; }
      [JsonPropertyName("Direccion")]
      public String? PersonDirection { get; set; }
      [JsonPropertyName("Telefono")]
      public String? PersonPhone { get; set; }
      [JsonPropertyName("Contrasena")]
      public String? ClientPassword { get; set; }
      [JsonPropertyName("Estado")]
      public bool ClientState { get; set; }
   }
}
