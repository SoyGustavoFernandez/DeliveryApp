using RoleService.Domain.Enums;

namespace RoleService.Domain.DTOs.Role
{
    public class RoleResponseDTO
    {
        /// <summary>
        /// ID único del rol
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nombre del rol
        /// </summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>
        /// Descripción del rol
        /// </summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>
        /// Lista de permisos asignados (simplificada)
        /// </summary>
        public List<PermisoSimplificado> Permisos { get; set; } = [];

        /// <summary>
        /// Total de usuarios con este rol
        /// </summary>
        public int TotalUsuarios { get; set; }
    }
}