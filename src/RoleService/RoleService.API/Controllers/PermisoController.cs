using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoleService.Application.Commands.Permisos;
using RoleService.Application.Queries.Permisos;
using RoleService.Domain.DTOs.Permiso;

namespace RoleService.API.Controllers
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

            return result.Success ? Ok(result) : NotFound(result);
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

            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Registra un nuevo permiso en la aplicación.
        /// </summary>
        /// <param name="dto">Datos del permiso a registrar</param>
        /// <returns>Devuelve 201 si el permiso se creó correctamente</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearPermiso([FromBody] CreatePermisoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El DTO no puede ser nulo.");
            }

            var command = new CreatePermisoCommand(dto.Nombre, dto.Descripcion);
            var response = await mediator.Send(command);

            return response.Success ? CreatedAtAction(nameof(CrearPermiso), new { id = response.Data }, response) : BadRequest(response);
        }

        /// <summary>
        /// Actualiza los datos de un permiso.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Devuelve 200 si el permiso se actualizó correctamente</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarPermiso(Guid id, [FromBody] UpdatePermisoDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El DTO no puede ser nulo.");
            }

            var command = new UpdatePermisoCommand(id, dto.Nombre, dto.Descripcion);
            var response = await mediator.Send(command);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Elimina un permiso por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve 200 si el permiso se eliminó correctamente</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarPermiso(Guid id)
        {
            var response = await mediator.Send(new DeletePermisoCommand(id));

            return response.Success ? Ok(response) : NotFound(response.Message);
        }
    }
}