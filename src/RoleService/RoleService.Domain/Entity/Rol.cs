using DeliveryApp.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace RoleService.Domain.Entity
{
    /// <summary>
    /// Representa un rol en el sistema que agrupa permisos específicos.
    /// </summary>
    /// <remarks>
    /// Los roles son fundamentales para el sistema de autorización y pueden ser asignados a múltiples usuarios.
    /// Cada rol puede tener múltiples permisos asociados a través de la relación RolPermiso.
    /// </remarks>
    public class Rol: Auditoria
    {
        /// <summary>
        /// Identificador único del rol en formato GUID.
        /// </summary>
        /// <example>d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9g</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre único del rol. Campo requerido con máximo 100 caracteres.
        /// </summary>
        /// <example>Administrador</example>
        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción detallada del rol y sus capacidades. Máximo 1000 caracteres.
        /// </summary>
        /// <example>Rol con acceso completo a todas las funcionalidades del sistema</example>
        [MaxLength(1000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Colección de relaciones UsuarioRol que representan los usuarios que tienen este rol asignado.
        /// </summary>
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = [];

        /// <summary>
        /// Colección de relaciones RolPermiso que representan los permisos asignados a este rol.
        /// </summary>
        public ICollection<RolPermiso> RolPermisos { get; set; } = [];
    }
}