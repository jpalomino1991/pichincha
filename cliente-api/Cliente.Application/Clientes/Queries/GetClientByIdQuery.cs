using Cliente.Application.Response;
using MediatR;

namespace Cliente.Application.Clientes.Queries
{
   public class GetClientByIdQuery : IRequest<GetClientResponseModel>
   {
      public int Id { get; set; }

      public GetClientByIdQuery(int id)
      {
         Id = id;
      }
   }
}
