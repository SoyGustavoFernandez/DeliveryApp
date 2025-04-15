using DeliveryApp.Common.Entities;
using MediatR;

namespace RoleService.Application.Commands.Role
{
    /// <summary>
    /// Comando para actualizar un rol en la base de datos.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="id">Identificador del registro.</param>
    /// <param name="nombre">Nombre del rol.</param>
    /// <param name="descripcion">Descripción breve del rol.</param>
    public class UpdateRoleCommand(Guid id, string nombre, string descripcion, List<Guid>? permisoIds) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public Guid Id { get; init; } = id;

        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string Nombre { get; init; } = nombre;

        /// <summary>
        /// Descripción del rol
        /// </summary>
        public string Descripcion { get; init; } = descripcion;

        /// <summary>
        /// IDs de permisos a asignar al rol (opcional)
        /// </summary>
        public List<Guid>? Permisos { get; } = permisoIds;
    }
}