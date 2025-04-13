using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.Commands.Permisos;
using UserService.Application.Queries.Permisos;

namespace UserService.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermisoController(IMediator mediator) : ControllerBase
    {
        /// <summary>
        /// Lista todos los permisos.
        /// </summary>
        /// <returns>Devuelve una lista de permisos</returns>
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosPermisos()
        {
            var result = await mediator.Send(new GetPermisosQueries());
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }

        /// <summary>
        /// Lista un permiso por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve un permiso</returns>
        [HttpGet("{id}")]

        public async Task<IActionResult> ObtenerPermisoPorId(Guid id)
        {
            var result = await mediator.Send(new GetPermisoByIdQueries(id));
            if (!result.Success)
            {
                return NotFound(result);
            }
            return Ok(result);
        }
        /// <summary>
        /// Registra un nuevo permiso en la aplicación.
        /// </summary>
        /// <param name="command">Datos del permiso a registrar</param>
        /// <returns>Devuelve 201 si el permiso se creó correctamente</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearPermiso([FromBody] CreatePermisoCommand command)
        {
            if (command == null)
            {
                return BadRequest("El comando no puede ser nulo.");
            }
            var response = await mediator.Send(command);
            if (response.Success)
            {
                return CreatedAtAction(nameof(CrearPermiso), new { id = response.Data }, response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Actualiza los datos de un permiso.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>Devuelve 200 si el permiso se actualizó correctamente</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarPermiso(string id, [FromBody] UpdatePermisoCommand command)
        {
            if (command == null)
            {
                return BadRequest("El comando no puede ser nulo.");
            }

            command.Id = Guid.Parse(id);

            var response = await mediator.Send(command);
            if (response.Success)
            {
                return Ok(response);
            }
            return BadRequest(response.Message);
        }

        /// <summary>
        /// Elimina un permiso por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve 200 si el permiso se eliminó correctamente</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarPermiso(string id)
        {
            var response = await mediator.Send(new DeletePermisoCommand(Guid.Parse(id)));
            if (response.Success)
            {
                return Ok(response);
            }
            return NotFound(response.Message);
        }
    }
}