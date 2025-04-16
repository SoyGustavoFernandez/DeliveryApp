namespace RoleService.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRoleRepository RoleRepository { get; }
        IPermisoRepository PermisoRepository { get; }
        Task<int> CompleteAsync();
    }
}