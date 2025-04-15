using DeliveryApp.Common.Entities;
using MediatR;

namespace RoleService.Application.Commands.Role
{
    /// <summary>
    /// Comando para crear un nuevo rol en la base de datos.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="nombre">Nombre del rol.</param>
    /// <param name="descripcion">Descripción breve del rol.</param>
    public class CreateRoleCommand(string nombre, string descripcion, List<Guid>? permisoIds) : IRequest<Response<string>>
    {
        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string Nombre { get; set; } = nombre;

        /// <summary>
        /// Descripción del rol
        /// </summary>
        public string Descripcion { get; set; } = descripcion;

        /// <summary>
        /// IDs de permisos a asignar al rol (opcional)
        /// </summary>
        public List<Guid>? Permisos { get; } = permisoIds;
    }
}