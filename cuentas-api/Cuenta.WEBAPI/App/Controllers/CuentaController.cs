using Cuenta.Application.Cuentas.Commands;
using Cuenta.Application.Cuentas.Queries;
using Cuenta.Application.Response;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Cuenta.WEBAPI.App.Controllers
{
		[ApiController]
		[Route("cuentas")]
		public class CuentaController : Controller
		{
				 private readonly IMediator _mediator;

				 public CuentaController(IMediator mediator)
				 {
						_mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
				 }

				 [HttpGet("{id}")]
				 public async Task<ActionResult<GetAccountResponseModel>> ObtenerCuenta(String? id)
				 {
						return await _mediator.Send(new GetAccountByIdQuery(id));
				 }

				 [HttpPost]
				 public async Task<ActionResult<CreateAccountResponseModel>> CrearCuenta(CreateAccountCommand command)
				 {
						var result = await _mediator.Send(command);
						return StatusCode(201, result);
				 }

				 [HttpPut("{id}")]
				 public async Task<ActionResult<CreateAccountResponseModel>> ActualizarCuenta(String? id, UpdateAccountCommand command)
				 {
						command.AccountId = id;
						return await _mediator.Send(command);
				 }

				 [HttpDelete("{id}")]
				 public async Task<ActionResult<String?>> EliminarCuenta(String? id)
				 {
						await _mediator.Send(new DeleteAccountCommand { Id = id });
						return Ok();
				 }
	 }
}
