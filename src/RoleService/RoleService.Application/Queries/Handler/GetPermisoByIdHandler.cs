using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;
using RoleService.Application.Queries.Permisos;
using RoleService.Domain.DTOs;

namespace RoleService.Application.Queries.Handler
{
    /// <summary>
    /// Handler para obtener un permiso por su identificador.
    /// </summary>
    public class GetPermisoByIdHandler(IPermisoRepository repository) : IRequestHandler<GetPermisoByIdQueries, Response<PermisoResponseDTO>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<PermisoResponseDTO>> Handle(GetPermisoByIdQueries request, CancellationToken cancellationToken)
        {
            var permiso = await _repository.GetPermisoByIdAsync(request.Id);

            if (permiso == null)
            {
                return new Response<PermisoResponseDTO>(false, "Permiso no encontrado", null!, (int)HttpStatusCode.NotFound);
            }

            var permisoDTO = new PermisoResponseDTO
            {
                Id = permiso.Id,
                Nombre = permiso.Nombre,
                Descripcion = permiso.Descripcion
            };

            return new Response<PermisoResponseDTO>(true, "Permiso encontrado", permisoDTO, (int)HttpStatusCode.OK);
        }
    }
}