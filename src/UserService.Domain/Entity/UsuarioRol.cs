using DeliveryApp.Common.Entities;

namespace UserService.Domain.Entity
{
    /// <summary>
    /// Tabla de unión que relaciona usuarios con roles (relación many-to-many).
    /// </summary>
    /// <remarks>
    /// Esta entidad permite la asignación de múltiples roles a un usuario y viceversa.
    /// Representa la relación entre las entidades Usuario y Rol.
    /// </remarks>
    public class UsuarioRol: Auditoria
    {
        /// <summary>
        /// Identificador único del usuario relacionado.
        /// </summary>
        /// <example>a3b2c1d0-e5f4-4a7b-8c3d-2e1f0a9b8c7d</example>
        public Guid UsuarioId { get; private set; }

        /// <summary>
        /// Objeto Usuario relacionado.
        /// </summary>
        public Usuario Usuario { get; private set; }

        /// <summary>
        /// Identificador único del rol relacionado.
        /// </summary>
        /// <example>d4e5f6a7-b8c9-0d1e-2f3a-4b5c6d7e8f9g</example>
        public Guid RolId { get; private set; }

        /// <summary>
        /// Objeto Rol relacionado.
        /// </summary>
        public Rol Rol { get; private set; }
    }
}