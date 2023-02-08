using Cuenta.Application.Response;
using System.Net.Http.Json;

namespace Cuenta.Application.Common.Services
{
   public class ClientService : IClientService
   {
      private readonly HttpClient _httpClient;
      public ClientService(HttpClient httpClient)
      {
         _httpClient = httpClient;
      }
      public async Task<ClientResponseModel> GetClientByName(String? Name)
      {
         try
         {
            string uri = String.Concat(_httpClient.BaseAddress , "name/", Name);
            return await _httpClient.GetFromJsonAsync<ClientResponseModel>(uri);
         }
         catch(HttpRequestException) {
            throw new NullReferenceException("Client doesn't exists");
         }
      }

      public async Task<ClientResponseModel> GetClientById(int id)
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
