using UserService.Domain.Entity;

namespace UserService.Application.Interfaces
{
    public interface IPermisoRepository
    {
        Task AddPermisoAsync(Permiso permiso);
        Task UpdatePermisoAsync(Permiso permiso);
        Task DeletePermisoAsync(Permiso permiso);
        Task<Permiso> GetPermisoByIdAsync(Guid id);
        Task<List<Permiso>> GetAllPermisosAsync();
    }
}