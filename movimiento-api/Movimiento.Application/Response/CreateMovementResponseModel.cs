namespace Movimiento.Application.Response
{
   public class CreateMovementResponseModel
   {
      public int MovementId { get; set; }
      public String? AccountId { get; set; }
      public String? MovementType { get; set; }
      public double MovementAmount { get; set; }
      public double MovementBalance { get; set; }
   }
}
