using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Entity;
using UserService.Infrastructure.Data;

namespace UserService.Infrastructure.Repositories
{
    public class PermisoRepository(ApplicationDbContext context) : IPermisoRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddPermisoAsync(Permiso permiso)
        {
            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermisoAsync(Permiso permiso)
        {
            _context.Permisos.Remove(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Permiso>> GetAllPermisosAsync()
        {
            return await _context.Permisos.Where(p => p.Vigente).Select(p => new Permiso
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion
            }).ToListAsync();
        }

        public async Task<Permiso> GetPermisoByIdAsync(Guid id)
        {
            var permiso = await _context.Permisos.Where(p => p.Id == id && p.Vigente).FirstOrDefaultAsync();
            return permiso;
        }

        public async Task UpdatePermisoAsync(Permiso permiso)
        {
            _context.Permisos.Update(permiso);
            await _context.SaveChangesAsync();
        }
    }
}