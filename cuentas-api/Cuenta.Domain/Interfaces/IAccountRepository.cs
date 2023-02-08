using Cuenta.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cuenta.Domain.Interfaces
{
   public interface IAccountRepository : IAsyncRepository<AccountEntity>
   {
      Task<bool> AccountExists(String? accountId);
   }
}
