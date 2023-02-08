using Cliente.Domain.Entities;
using Cliente.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cliente.Infraestructure.Data.Repositories
{
   public class PersonRepository : RepositoryBase<PersonEntity>, IPersonRepository
   {
      public PersonRepository(ClientContext dbContext) : base(dbContext)
      {

      }

      public async Task<bool> ClientExists(PersonEntity person)
      {
         return await DbSet.AnyAsync(p => p.PersonName.Equals(person.PersonName) || p.PersonPhone.Equals(person.PersonPhone));
      }
   }
}
