using Cliente.Application.Clientes.Commands;
using Cliente.Application.Clientes.Queries;
using Cliente.Application.Cuentas.Commands;
using Cliente.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cliente.WEBAPI.App.Controllers
{
	 [ApiController]
	 [Route("clientes")]
	 public class ClienteController : Controller
	 {
			private readonly IMediator _mediator;

			public ClienteController(IMediator mediator)
			{
				 _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
			}

      [HttpGet("name/{name}")]
      public async Task<ActionResult<GetClientResponseModel>> ObtenerCliente(String? name)
      {
         return await _mediator.Send(new GetClientByNameQuery(name));
      }

      [HttpGet("{id:int}")]
      public async Task<ActionResult<GetClientResponseModel>> ObtenerCliente(int id)
      {
         return await _mediator.Send(new GetClientByIdQuery(id));
      }

      [HttpPost]
			public async Task<ActionResult<CreateClientResponseModel>> CrearCuenta(CreateClientCommand command)
			{
				 var result = await _mediator.Send(command);
				 return StatusCode(201, result);
			}

			[HttpPut("{id}")]
			public async Task<ActionResult<CreateClientResponseModel>> ActualizarCuenta(int id, UpdateClientCommand command)
			{
				 command.Id = id;
				 return await _mediator.Send(command);
			}

			[HttpDelete("{id}")]
			public async Task<ActionResult<int>> EliminarCuenta(int id)
			{
				 await _mediator.Send(new DeleteClientCommand { Id = id });
				 return Ok();
			}
	 }
}
