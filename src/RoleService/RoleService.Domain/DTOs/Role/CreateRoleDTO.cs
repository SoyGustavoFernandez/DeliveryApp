using System.ComponentModel.DataAnnotations;

namespace RoleService.Domain.DTOs.Role
{
    /// <summary>
    /// Clase para crear un nuevo rol en el sistema.
    /// </summary>
    public class CreateRoleDTO
    {
        /// <summary>
        /// Nombre único del rol. Ejemplo: "Administrador"
        /// </summary>
        [Required(ErrorMessage = "El nombre del rol es requerido")]
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del rol. Ejemplo: "Acceso completo al sistema"
        /// </summary>
        [MaxLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// IDs de permisos a asignar al rol (opcional)
        /// </summary>
        /// <example>["b2c3d4e5-f6g7-4a8b-9c0d-1e2f3a4b5c6d"]</example>
        public List<Guid>? PermisoIds { get; set; } = [];
    }
}