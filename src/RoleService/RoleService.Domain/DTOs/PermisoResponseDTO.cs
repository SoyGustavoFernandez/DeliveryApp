
namespace RoleService.Domain.DTOs
{
    /// <summary>
    /// Representa un permiso específico en el sistema.
    /// </summary>
    public class PermisoResponseDTO
    {
        /// <summary>
        /// ID único del permiso
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del permiso en formato 'recurso.accion'
        /// </summary>
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción del permiso
        /// </summary>
        public string Descripcion { get; set; }
    }
}