using System.ComponentModel.DataAnnotations;

namespace RoleService.Domain.DTOs
{
    /// <summary>
    /// Clase para crear un nuevo permiso en el sistema.
    /// </summary>
    public class CreatePermisoDTO
    {
        /// <summary>
        /// Nombre del permiso en formato 'recurso.accion'. Ejemplo: usuarios.crear
        /// </summary>
        [Required(ErrorMessage = "El nombre del permiso es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        [RegularExpression(@"^[a-z]+\.[a-z_]+$", ErrorMessage = "Formato inválido. Use 'recurso.accion'")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción detallada del permiso. Ejemplo: Permite crear nuevos usuarios
        /// </summary>
        [MaxLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string Descripcion { get; set; }
    }
}