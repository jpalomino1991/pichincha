using Cliente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Response
{
   public class GetClientResponseModel
   {
      public int ClientId { get; set; }
      public bool ClientState { get; set; }
      public virtual PersonEntity? Person { get; set; }
   }
}
