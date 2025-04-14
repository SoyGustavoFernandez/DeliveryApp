using System.ComponentModel.DataAnnotations;

namespace RoleService.Domain.DTOs
{
    /// <summary>
    /// Clase para actualizar un permiso existente.
    /// </summary>
    public class UpdatePermisoDTO
    {
        /// <summary>
        /// Nuevo nombre del permiso (opcional)
        /// </summary>
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [RegularExpression(@"^[a-z]+\.[a-z_]+$", ErrorMessage = "Formato inválido. Use 'recurso.accion'")]
        public string? Nombre { get; set; }

        /// <summary>
        /// Nueva descripción del permiso (opcional)
        /// </summary>
        [MaxLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string? Descripcion { get; set; }
    }
}
