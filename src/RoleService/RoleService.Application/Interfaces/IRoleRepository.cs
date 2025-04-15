using RoleService.Domain.Entity;

namespace RoleService.Application.Interfaces
{
    public interface IRoleRepository
    {
        Task AddRoleAsync(Rol rol);
        Task UpdateRoleAsync(Rol rol);
        Task DeleteRoleAsync(Rol rol);
        Task<Rol> GetRoleByIdAsync(Guid id, bool incluirRelaciones);
        Task<List<Rol>> GetAllRolesAsync(bool incluirRelaciones);
        Task UpdatePermisosAsync(Rol rol, List<Permiso> nuevosPermisoIds);
        Task AddPermisosAsync(Rol rol, List<Permiso> permisos);
        Task RemoveRangePermisos(ICollection<RolPermiso> permisos);
    }
}
