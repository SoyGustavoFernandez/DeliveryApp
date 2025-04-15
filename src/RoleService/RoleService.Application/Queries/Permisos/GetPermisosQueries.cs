using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Domain.DTOs.Permiso;

namespace RoleService.Application.Queries.Permisos
{
    /// <summary>
    /// Query para obtener todos los permisos activos en BD.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    public class GetPermisosQueries : IRequest<Response<List<PermisoResponseDTO>>>
    {
    }
}