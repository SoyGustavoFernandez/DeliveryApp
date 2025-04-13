using DeliveryApp.Common.Entities;

namespace UserService.Domain.Entity
{
    /// <summary>
    /// Tabla de unión que relaciona roles con permisos (relación many-to-many).
    /// </summary>
    /// <remarks>
    /// Esta entidad permite la asignación de múltiples permisos a un rol y viceversa.
    /// Representa la relación entre las entidades Rol y Permiso.
    /// </remarks>
    public class RolPermiso: Auditoria
    {
        /// <summary>
        /// Identificador único del rol relacionado.
        /// </summary>
        /// <example>d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9g</example>
        public Guid RolId { get; private set; }

        /// <summary>
        /// Objeto Rol relacionado.
        /// </summary>
        public Rol Rol { get; private set; }

        /// <summary>
        /// Identificador único del permiso relacionado.
        /// </summary>
        /// <example>b2c3d4e5-f6g7-4a8b-9c0d-1e2f3a4b5c6d</example>
        public Guid PermisoId { get; private set; }

        /// <summary>
        /// Objeto Permiso relacionado.
        /// </summary>
        public Permiso Permiso { get; private set; }
    }
}