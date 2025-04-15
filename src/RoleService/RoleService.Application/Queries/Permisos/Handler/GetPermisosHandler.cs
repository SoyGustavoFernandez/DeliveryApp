using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;
using RoleService.Domain.DTOs.Permiso;

namespace RoleService.Application.Queries.Permisos.Handler
{
    /// <summary>
    /// Handler para obtener todos los permisos activos en BD.
    /// </summary>
    public class GetPermisosHandler(IPermisoRepository repository) : IRequestHandler<GetPermisosQueries, Response<List<PermisoResponseDTO>>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<List<PermisoResponseDTO>>> Handle(GetPermisosQueries request, CancellationToken cancellationToken)
        {
            var permisos = await _repository.GetAllPermisosAsync();

            if (permisos == null || permisos.Count == 0)
            {
                return new Response<List<PermisoResponseDTO>>(false, "No se encontraron permisos", null!, (int)HttpStatusCode.NotFound);
            }

            var permisosDTO = permisos.Select(p => new PermisoResponseDTO
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion
            }).ToList();

            return new Response<List<PermisoResponseDTO>>(true, "Permisos encontrados", permisosDTO, (int)HttpStatusCode.OK);
        }
    }
}