using Movimiento.Domain.Entities;
using Movimiento.Domain.Interfaces;

namespace Movimiento.Infraestructure.Data.Repositories
{
   public class MovementRepository : RepositoryBase<MovementEntity>, IMovementRepository
   {
      public MovementRepository(MovimientoContext dbContext) : base(dbContext)
      {

      }
   }
}
