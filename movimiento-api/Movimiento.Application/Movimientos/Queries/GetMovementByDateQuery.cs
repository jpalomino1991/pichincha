using MediatR;
using Movimiento.Application.Response;
using System.Text.Json.Serialization;

namespace Movimiento.Application.Movimientos.Queries
{
   public class GetMovementByDateQuery : IRequest<List<GetMovementByDataResponseModel>>
   {
      [JsonIgnore]
      public int ClientId { get; set; }
      [JsonPropertyName("Fecha Inicio")]
      public DateTime? BeginDate { get; set; }
      [JsonPropertyName("Fecha Fin")]
      public DateTime? EndDate { get; set; }
   }
}
