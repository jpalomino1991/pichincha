using MediatR;

namespace Movimiento.Application.Movimientos.Commands
{
   public class DeleteMovementCommand : IRequest<int>
   {
      public int Id { get; set; }
   }
}
