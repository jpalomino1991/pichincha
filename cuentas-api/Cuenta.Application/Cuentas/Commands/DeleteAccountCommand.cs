using MediatR;

namespace Cuenta.Application.Cuentas.Commands
{
   public class DeleteAccountCommand : IRequest<String?>
   {
      public String? Id { get; set; }
   }
}
