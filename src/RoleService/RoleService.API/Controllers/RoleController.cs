using MediatR;
using Microsoft.AspNetCore.Mvc;
using RoleService.Application.Commands.Role;
using RoleService.Application.Queries.Role;
using RoleService.Domain.DTOs.Role;

namespace RoleService.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        /// <summary>
        /// Obtiene todos los roles activos en BD.
        /// </summary>
        /// <returns>Devuelve una lista de roles.</returns>
        /// 
        [HttpGet]
        public async Task<IActionResult> ObtenerTodosLosRoles(bool _incluirDetalles)
        {
            var result = await _mediator.Send(new GetRolesQueries(_incluirDetalles));

            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Obtiene un rol por su Id.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="incluirDetalles"></param>
        /// <returns>Devuelve un rol.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerRolPorId(Guid id, bool incluirDetalles)
        {
            var result = await _mediator.Send(new GetRoleByIdQueries(id, incluirDetalles));

            return result.Success ? Ok(result) : NotFound(result);
        }

        /// <summary>
        /// Registra un nuevo rol en la aplicación.
        /// </summary>
        /// <param name="dto">Datos del rol a registrar</param>
        /// <returns>Devuelve 201 si el rol se creó correctamente</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearRol([FromBody] CreateRoleDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El DTO no puede ser nulo.");
            }

            var command = new CreateRoleCommand(dto.Nombre, dto.Descripcion, dto.PermisoIds);
            var response = await _mediator.Send(command);
            
            return response.Success ? CreatedAtAction(nameof(ObtenerRolPorId), new { id = response.Data }, response) : BadRequest(response.Message);
        }

        /// <summary>
        /// Actualiza un rol existente en la aplicación.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns>Devuelve 200 si el rol se actualizó correctamente</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActuaizarRol(Guid id, [FromBody] UpdateRoleDTO dto)
        {
            if (dto == null)
            {
                return BadRequest("El DTO no puede ser nulo.");
            }

            var command = new UpdateRoleCommand(id, dto.Nombre, dto.Descripcion, dto.PermisoIds);
            var response = await _mediator.Send(command);

            return response.Success ? Ok(response) : BadRequest(response);
        }

        /// <summary>
        /// Elimina un rol existente en la aplicación.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Devuelve 200 si el rol se eliminó correctamente</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> EliminarRol(Guid id)
        {
            var command = new DeleteRoleCommand(id);
            var response = await _mediator.Send(command);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}