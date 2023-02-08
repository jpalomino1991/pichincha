using Cuenta.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuenta.Application.Common.Services
{
   public interface IClientService
   {
      Task<ClientResponseModel> GetClientByName(String? Name);
      Task<ClientResponseModel> GetClientById(int id);
   }
}
