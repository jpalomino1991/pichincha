using Cliente.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cliente.Application.Clientes.Commands
{
   public class UpdateClientCommand : IRequest<CreateClientResponseModel>
   {
      [JsonIgnore]
      public int Id { get; set; }
      [JsonPropertyName("Nombres")]
      public String? PersonName { get; set; }
      [JsonPropertyName("Direccion")]
      public String? PersonDirection { get; set; }
      [JsonPropertyName("Telefono")]
      public String? PersonPhone { get; set; }
   }
}
