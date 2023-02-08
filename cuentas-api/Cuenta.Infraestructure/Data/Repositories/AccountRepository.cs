using Cuenta.Domain.Entities;
using Cuenta.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cuenta.Infraestructure.Data.Repositories
{
   public class AccountRepository : RepositoryBase<AccountEntity>, IAccountRepository
   {
      public AccountRepository(AccountContext dbContext) : base(dbContext)
      {

      }

      public async Task<bool> AccountExists(string? accountId)
      {
         var entity = await DbSet.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.AccountId.Equals(accountId));
         return entity != null;
      }
   }
}
