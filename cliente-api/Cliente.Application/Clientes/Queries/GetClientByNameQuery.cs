using Cliente.Application.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cliente.Application.Clientes.Queries
{
   public class GetClientByNameQuery : IRequest<GetClientResponseModel>
   {
      public String? Name { get; set; }

      public GetClientByNameQuery(String? name)
      {
         Name = name;
      }
   }
}
