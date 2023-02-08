using MediatR;
using Microsoft.AspNetCore.Mvc;
using Movimiento.Application.Movimientos.Commands;
using Movimiento.Application.Movimientos.Queries;
using Movimiento.Application.Response;

namespace Movimiento.WEBAPI.App.Controllers
{
	 [ApiController]
	 [Route("movimientos")]
	 public class MovimientoController : Controller
	 {
				 private readonly IMediator _mediator;

				 public MovimientoController(IMediator mediator)
				 {
						_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
				 }

				 [HttpPost]
				 public async Task<ActionResult<CreateMovementResponseModel>> CrearMovimiento(CreateMovementCommand command)
				 {
						var result = await _mediator.Send(command);
						return StatusCode(201, result);
				 }

				 [HttpPost("reporte/{id}")]
				 public async Task<ActionResult<List<GetMovementByDataResponseModel>>> Reporte(int id, GetMovementByDateQuery query)
				 {
						query.ClientId = id;
						return await _mediator.Send(query);
				 }

				 [HttpDelete("{id}")]
				 public async Task<ActionResult<int>> EliminarCuenta(int id)
				 {
						await _mediator.Send(new DeleteMovementCommand { Id = id });
						return Ok();
				 }
	 }
}
