using Account.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cuenta.Domain.Entities
{
   [Table("Account", Schema = "public")]
   public class AccountEntity : BaseEntity
   {
      [StringLength(20)]
      public String? AccountId { get; set; }
      [Required]
      [StringLength(20)]
      public String? AccountType { get; set; }
      [Required]
      public double AccountAmount { get; set; }
      [Required]
      public bool AccountState { get; set; }
      [Required]
      public int ClientId { get; set; }
      [NotMapped]
      public String? ClientName { get; set; }
   }
}
