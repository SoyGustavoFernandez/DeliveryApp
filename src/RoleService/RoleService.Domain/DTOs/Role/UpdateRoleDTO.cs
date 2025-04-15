using System.ComponentModel.DataAnnotations;

namespace RoleService.Domain.DTOs.Role
{
    public class UpdateRoleDTO
    {
        /// <summary>
        /// Nuevo nombre del rol (opcional)
        /// </summary>
        /// <example>Administrador</example>
        [MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Nueva descripción del rol (opcional)
        /// </summary>
        /// <example>Rol con acceso completo al sistema</example>
        [MaxLength(1000, ErrorMessage = "La descripción no puede exceder 1000 caracteres")]
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// IDs de permisos para actualizar (opcional)
        /// </summary>
        public List<Guid>? PermisoIds { get; set; }
    }
}