using DeliveryApp.Common.Entities;
using MediatR;
using UserService.Domain.Entity;

namespace UserService.Application.Queries.Permisos
{
    /// <summary>
    /// Query para obtener todos los permisos activos en BD.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    public class GetPermisosQueries : IRequest<Response<List<Permiso>>>
    {
    }
}