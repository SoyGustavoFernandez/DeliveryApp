using Microsoft.EntityFrameworkCore;
using RoleService.Application.Interfaces;
using RoleService.Domain.Entity;
using RoleService.Infrastructure.Data;

namespace RoleService.Infrastructure.Repositories
{
    public class RoleRepository(ApplicationDbContext context) : IRoleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task AddPermisosAsync(Rol rol, List<Permiso> permisos)
        {
            foreach (var permiso in permisos)
            {
                if (!permiso.Vigente)
                    continue;

                var existente = rol.RolPermisos.FirstOrDefault(rp => rp.PermisoId == permiso.Id);

                if (existente != null)
                {
                    if (!existente.Vigente)
                    {
                        existente.Vigente = true;
                        existente.FecMod = DateTime.Now;
                    }
                }
                else
                {
                    rol.RolPermisos.Add(new RolPermiso
                    {
                        RolId = rol.Id,
                        PermisoId = permiso.Id,
                        IdUsuReg = Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88")
                    });
                }
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddRoleAsync(Rol rol)
        {
            _context.Roles.Add(rol);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(Rol rol)
        {
            _context.Roles.Remove(rol);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Rol>> GetAllRolesAsync(bool incluirRelaciones)
        {
            var query = _context.Roles.AsNoTracking().Where(r => r.Vigente);

            if (incluirRelaciones)
            {
                query = query.Include(r => r.RolPermisos.Where(rp => rp.Vigente)).ThenInclude(rp => rp.Permiso).Include(r => r.UsuarioRoles);
            }

            return await query.ToListAsync();
        }

        public async Task<Rol> GetRoleByIdAsync(Guid id, bool incluirRelaciones)
        {
            var query = _context.Roles.Where(r => r.Id == id && r.Vigente);

            if (incluirRelaciones)
            {
                query = query.Include(r => r.RolPermisos.Where(rp => rp.Vigente)).ThenInclude(rp => rp.Permiso).Include(r => r.UsuarioRoles);
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task RemoveRangePermisos(ICollection<RolPermiso> permisos)
        {
            if (permisos.Count == 0) return;

            var rolId = permisos.First().RolId;
            var permisoIds = permisos.Select(p => p.PermisoId).ToList();

            var permisosFromDb = await _context.RolPermisos.Where(rp => rp.RolId == rolId && permisoIds.Contains(rp.PermisoId) && rp.Vigente).ToListAsync();

            foreach (var permiso in permisosFromDb)
            {
                permiso.Vigente = false;
                permiso.FecMod = DateTime.UtcNow;
            }
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePermisosAsync(Rol rol, List<Permiso> nuevosPermisos)
        {
            var rolId = rol.Id;

            var nuevosPermisoIds = nuevosPermisos.Select(p => p.Id).ToHashSet();

            var permisosActuales = await _context.RolPermisos.Where(rp => rp.RolId == rolId).ToListAsync();

            foreach (var permiso in permisosActuales)
            {
                permiso.Vigente = false;
                permiso.FecMod = DateTime.UtcNow;
            }

            foreach (var permisoId in nuevosPermisoIds)
            {
                var permisoVigente = await _context.Permisos.Where(p => p.Id == permisoId && p.Vigente).FirstOrDefaultAsync();

                if (permisoVigente == null)
                    continue;

                var existente = permisosActuales.FirstOrDefault(rp => rp.PermisoId == permisoId);

                if (existente != null)
                {
                    existente.Vigente = true;
                    existente.FecMod = DateTime.UtcNow;
                }
                else
                {
                    rol.RolPermisos.Add(new RolPermiso
                    {
                        RolId = rolId,
                        PermisoId = permisoId
                    });
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(Rol rol)
        {
            _context.Roles.Update(rol);
            await _context.SaveChangesAsync();
        }
    }
}