using Cliente.Domain.Entities;

namespace Cliente.Domain.Interfaces
{
   public interface IPersonRepository : IAsyncRepository<PersonEntity>
   {
      Task<bool> ClientExists(PersonEntity p);
   }
}
