using DeliveryApp.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entity
{
    /// <summary>
    /// Representa un permiso específico en el sistema.
    /// </summary>
    /// <remarks>
    /// Los permisos definen acciones específicas que pueden ser realizadas en el sistema.
    /// Se asignan a roles, no directamente a usuarios.
    /// </remarks>
    public class Permiso : Auditoria
    {
        /// <summary>
        /// Identificador único del permiso en formato GUID.
        /// </summary>
        /// <example>b2c3d4e5-f6g7-4a8b-9c0d-1e2f3a4b5c6d</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre único del permiso en formato 'recurso.accion'. Campo requerido con máximo 100 caracteres.
        /// </summary>
        /// <example>usuarios.crear</example>
        [Required]
        [MaxLength(100)]
        [RegularExpression(@"^[a-z]+\.[a-z_]+$", ErrorMessage = "Formato inválido. Use 'recurso.accion'")]
        public string Nombre { get; set; }

        /// <summary>
        /// Descripción detallada del permiso y su alcance. Máximo 1000 caracteres.
        /// </summary>
        /// <example>Permite crear nuevos usuarios en el sistema</example>
        [MaxLength(1000)]
        public string Descripcion { get; set; }

        /// <summary>
        /// Colección de relaciones RolPermiso que representan los roles que tienen este permiso asignado.
        /// </summary>
        public ICollection<RolPermiso> RolPermisos { get; set; } = new List<RolPermiso>();
    }
}