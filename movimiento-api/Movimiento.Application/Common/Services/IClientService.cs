using Movimiento.Application.Response;

namespace Movimiento.Application.Common.Services
{
   public interface IClientService
   {
      Task<ClientResponseModel> GetClient(int id);
   }
}
