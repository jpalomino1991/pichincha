using Movimiento.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movimiento.Domain.Interfaces
{
   public interface IMovementRepository : IAsyncRepository<MovementEntity>
   {
   }
}
