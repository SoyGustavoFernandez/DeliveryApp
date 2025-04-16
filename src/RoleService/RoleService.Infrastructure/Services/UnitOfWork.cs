using RoleService.Application.Interfaces;
using RoleService.Infrastructure.Data;

namespace RoleService.Infrastructure.Services
{
    public class UnitOfWork(ApplicationDbContext context, IRoleRepository roleRepository, IPermisoRepository permisoRepository) : IUnitOfWork
    {
        private readonly ApplicationDbContext _context = context;

        public IRoleRepository RoleRepository { get; } = roleRepository;
        public IPermisoRepository PermisoRepository { get; } = permisoRepository;

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}