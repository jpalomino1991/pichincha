using Movimiento.Domain.Entities.Base;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Movimiento.Domain.Entities
{
   [Table("Movement", Schema = "public")]
   public class MovementEntity : BaseEntity
   {
      [Required]
      public int MovementId { get; set; }
      [Required]
      public DateTime MovementDate { get; set; }
      [Required]
      [StringLength(20)]
      public String? MovementType { get; set; }
      public bool MovementState { get; set; }
      [Required]
      public double MovementAmount { get; set; }
      [Required]
      public double MovementBalance { get; set; }
      [Required]
      public String? AccountId { get; set; }
      [NotMapped]
      public String? ClientName { get; set; }
   }
}
