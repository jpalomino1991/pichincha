using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cliente.Domain.Entities.Base;

namespace Cliente.Domain.Entities
{
   [Table("Client", Schema = "public")]
   public class ClientEntity : BaseEntity
   {
      public int ClientId { get; set; }
      [Required]
      [DataType(DataType.Password)]
      [StringLength(20)]
      public String? ClientPassword { get; set; }
      public bool ClientState { get; set; }
      public int PersonId { get; set; }
      public virtual PersonEntity? Person { get; set; }
   }
}
