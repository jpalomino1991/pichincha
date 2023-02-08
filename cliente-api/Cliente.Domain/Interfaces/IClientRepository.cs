using Cliente.Domain.Entities;

namespace Cliente.Domain.Interfaces
{
   public interface IClientRepository : IAsyncRepository<ClientEntity>
   {
      Task<ClientEntity> GetClientByName(String? name);
      Task<ClientEntity> GetClientById(int id);
   }
}
