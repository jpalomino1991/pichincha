using Movimiento.Application.Common.Dtos;
using Movimiento.Application.Response;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Movimiento.Application.Common.Services
{
   public class AccountService : IAccountService
   {
      private readonly HttpClient _httpClient;
      public AccountService(HttpClient httpClient)
      {
         _httpClient = httpClient;
      }
      public async Task<AccountResponseModel> GetAccount(String? accountId)
      {
         try
         {
            string uri = String.Concat(_httpClient.BaseAddress, accountId);
            return await _httpClient.GetFromJsonAsync<AccountResponseModel>(uri);
         }
         catch (HttpRequestException)
         {
            throw new NullReferenceException("Account doesn't exists");
         }
      }

      public async Task<AccountResponseModel> UpdateAccount(String? accountId,AccountDto account)
      {
         try
         {
            var acc = JsonSerializer.Serialize(account);
            var requestContent = new StringContent(acc, Encoding.UTF8, "application/json");
            string uri = String.Concat(_httpClient.BaseAddress, accountId);
            var response = await _httpClient.PutAsync(uri, requestContent);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<AccountResponseModel>(content);
         }
         catch (HttpRequestException)
         {
            throw new NullReferenceException("Account doesn't exists");
         }
      }
   }
}
