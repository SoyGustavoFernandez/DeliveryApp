using DeliveryApp.Common.Entities;
using MediatR;

namespace UserService.Application.Commands.Permisos
{
    /// <summary>
    /// Comando para crear un nuevo permiso en la base de datos.
    /// </summary>
    /// <remarks>
    /// Constructor
    /// </remarks>
    /// <param name="nombre">Nombre del permiso.</param>
    /// <param name="descripcion">Descripción breve del permiso.</param>
    public class CreatePermisoCommand(string nombre, string descripcion) : IRequest<Response<string>>
    {
        /// <summary>
        /// Nombre del permiso
        /// </summary>
        public string Nombre { get; set; } = nombre;

        /// <summary>
        /// Descripción del permiso
        /// </summary>
        public string Descripcion { get; set; } = descripcion;
    }
}