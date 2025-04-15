using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Domain.DTOs.Role;

namespace RoleService.Application.Queries.Role
{
    /// <summary>
    /// Consulta para obtener todos los roles.
    /// </summary>
    public class GetRolesQueries(bool incluirDetalles) : IRequest<Response<List<RoleResponseDTO>>>
    {
        /// <summary>
        /// Indica si se deben incluir detalles adicionales en la respuesta.
        /// </summary>
        public bool IncluirDetalles { get; set; } = incluirDetalles;
    }
}