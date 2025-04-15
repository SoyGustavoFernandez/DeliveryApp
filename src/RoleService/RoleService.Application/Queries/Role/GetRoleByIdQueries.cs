using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Domain.DTOs.Role;

namespace RoleService.Application.Queries.Role
{
    /// <summary>
    /// Consulta para obtener un rol por su identificador.
    /// </summary>
    /// <param name="guid"></param>
    public class GetRoleByIdQueries(Guid id, bool incluirDetalles) : IRequest<Response<RoleResponseDTO>>
    {
        /// <summary>
        /// Identificador del rol.
        /// </summary>
        public Guid Id { get; set; } = id;

        /// <summary>
        /// Indica si se deben incluir detalles adicionales en la respuesta.
        /// </summary>
        public bool IncluirDetalles { get; set; } = incluirDetalles;
    }
}