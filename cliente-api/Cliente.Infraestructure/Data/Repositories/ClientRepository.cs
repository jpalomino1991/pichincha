using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Infraestructure.Data.Repositories
{
   public class ClientRepository : RepositoryBase<ClientEntity>, IClientRepository
   {
      public ClientRepository(ClientContext dbContext) : base(dbContext)
      {

      }

      public async Task<ClientEntity?> GetClientByName(string? name)
      {
         return await DbSet.Include(c => c.Person).Where(c => c.Person.PersonName.Equals(name)).FirstOrDefaultAsync();
      }

      public async Task<ClientEntity?> GetClientById(int id)
      {
         return await DbSet.Include(c => c.Person).Where(c => c.ClientId == id).FirstOrDefaultAsync();
      }
   }
}
