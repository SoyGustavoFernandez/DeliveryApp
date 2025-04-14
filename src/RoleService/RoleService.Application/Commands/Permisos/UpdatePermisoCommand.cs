using DeliveryApp.Common.Entities;
using MediatR;

namespace RoleService.Application.Commands.Permisos
{
    /// <summary>
    /// Comando para actualizar un permiso en la base de datos.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="id">Identificador del registro.</param>
    /// <param name="nombre">Nombre del permiso.</param>
    /// <param name="descripcion">Descripción breve del permiso.</param>
    public class UpdatePermisoCommand(Guid id, string nombre, string descripcion) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        public Guid Id { get; init; } = id;

        /// <summary>
        /// Nombre del permiso
        /// </summary>
        public string Nombre { get; init; } = nombre;

        /// <summary>
        /// Descripción del permiso
        /// </summary>
        public string Descripcion { get; init; } = descripcion;
    }
}