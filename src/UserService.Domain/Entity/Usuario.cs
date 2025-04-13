using DeliveryApp.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace UserService.Domain.Entity
{
    /// <summary>
    /// Representa un usuario en el sistema con información personal, credenciales y estado.
    /// </summary>
    /// <remarks>
    /// Esta entidad almacena todos los datos necesarios para la autenticación y gestión de usuarios.
    /// Los usuarios pueden tener múltiples roles asignados a través de la relación UsuarioRol.
    /// </remarks>
    public class Usuario : Auditoria
    {
        /// <summary>
        /// Identificador único del usuario (GUID)
        /// </summary>
        /// <example>a3b2c1d0-e5f4-4a7b-8c3d-2e1f0a9b8c7d</example>
        public Guid Id { get; set; }

        /// <summary>
        /// Primer nombre del usuario
        /// </summary>
        /// <example>Juan</example>
        [Required]
        [MaxLength(50)]
        public string PrimerNombre { get; set; }

        /// <summary>
        /// Segundo nombre del usuario. Campo requerido con máximo 50 caracteres.
        /// </summary>
        /// <example>Carlos</example>
        [Required]
        [MaxLength(50)]
        public string SegundoNombre { get; set; }

        /// <summary>
        /// Apellido paterno del usuario. Campo requerido con máximo 50 caracteres.
        /// </summary>
        /// <example>Pérez</example>
        [Required]
        [MaxLength(50)]
        public string ApellidoPaterno { get; set; }

        /// <summary>
        /// Apellido materno del usuario. Campo requerido con máximo 50 caracteres.
        /// </summary>
        /// <example>Gómez</example>
        [Required]
        [MaxLength(50)]
        public string ApellidoMaterno { get; set; }

        /// <summary>
        /// Nombre completo del usuario, concatenando los nombres y apellidos.
        /// </summary>
        /// <example>Juan Carlos Pérez Gómez</example>
        public string NombreCompleto => $"{PrimerNombre} {SegundoNombre} {ApellidoPaterno} {ApellidoMaterno}";

        /// <summary>
        /// Correo electrónico único del usuario. Campo requerido con validación de formato email.
        /// </summary>
        /// <example>juan.perez@example.com</example>
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// Hash de la contraseña del usuario. Campo requerido.
        /// </summary>
        [Required]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Salt utilizado para el hash de la contraseña. Campo requerido.
        /// </summary>
        [Required]
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Número de teléfono del usuario. Máximo 20 caracteres.
        /// </summary>
        /// <example>+56912345678</example>
        [MaxLength(20)]
        public string Telefono { get; set; }

        /// <summary>
        /// Dirección física del usuario. Máximo 1000 caracteres.
        /// </summary>
        /// <example>Av. Principal 123, Piura, Perú</example>
        [MaxLength(1000)]
        public string Direccion { get; set; }

        /// <summary>
        /// Colección de relaciones UsuarioRol que representan los roles asignados a este usuario.
        /// </summary>
        public ICollection<UsuarioRol> UsuarioRoles { get; set; } = new List<UsuarioRol>();
    }
}