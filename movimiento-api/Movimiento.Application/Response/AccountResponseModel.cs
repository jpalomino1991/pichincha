namespace Movimiento.Application.Response
{
   public class AccountResponseModel
   {
      public String? AccountId { get; set; }
      public String? AccountType { get; set; }
      public double AccountAmount { get; set; }
      public bool AccountState { get; set; }
      public int ClientId { get; set; }
      public String? ClientName { get; set; }
   }
}
