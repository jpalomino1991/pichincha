using Movimiento.Application.Response;
using System.Net.Http.Json;

namespace Movimiento.Application.Common.Services
{
   public class ClientService : IClientService
   {
      private readonly HttpClient _httpClient;
      public ClientService(HttpClient httpClient)
      {
         _httpClient = httpClient;
      }
      public async Task<ClientResponseModel> GetClient(int id)
      {
         try
         {
            string uri = String.Concat(_httpClient.BaseAddress, id);
            return await _httpClient.GetFromJsonAsync<ClientResponseModel>(uri);
         }
         catch (HttpRequestException)
         {
            throw new NullReferenceException("Client doesn't exists");
         }
      }
   }
}
