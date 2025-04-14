using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;
using RoleService.Application.Queries.Permisos;
using RoleService.Domain.Entity;

namespace RoleService.Application.Queries.Handler
{
    /// <summary>
    /// Handler para obtener un permiso por su identificador.
    /// </summary>
    public class GetPermisoByIdHandler(IPermisoRepository repository) : IRequestHandler<GetPermisoByIdQueries, Response<Permiso>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<Permiso>> Handle(GetPermisoByIdQueries request, CancellationToken cancellationToken)
        {
            var permiso = await _repository.GetPermisoByIdAsync(request.Id);

            if (permiso == null)
            {
                return new Response<Permiso>(false, "Permiso no encontrado", null!, (int)HttpStatusCode.NotFound);
            }

            return new Response<Permiso>(true, "Permiso encontrado", permiso, (int)HttpStatusCode.OK);
        }
    }
}