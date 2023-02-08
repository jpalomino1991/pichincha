using Cuenta.Application.Common.Dtos;

namespace Cuenta.Application.Response
{
   public class ClientResponseModel
   {
      public int ClientId { get; set; }
      public bool ClientState { get; set; }
      public virtual PersonDto? Person { get; set; }
   }
}
