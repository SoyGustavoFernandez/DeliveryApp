using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Permisos;
using UserService.Domain.Entity;

namespace UserService.Application.Queries.Handler
{
    /// <summary>
    /// Handler para obtener todos los permisos activos en BD.
    /// </summary>
    public class GetPermisosHandler(IPermisoRepository repository) : IRequestHandler<GetPermisosQueries, Response<List<Permiso>>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<List<Permiso>>> Handle(GetPermisosQueries request, CancellationToken cancellationToken)
        {
            var permisos = await _repository.GetAllPermisosAsync();

            if (permisos == null || !permisos.Any())
            {
                return new Response<List<Permiso>>(false, "No se encontraron permisos", null!, (int)HttpStatusCode.NotFound);
            }

            return new Response<List<Permiso>>(true, "Permisos encontrados", permisos, (int)HttpStatusCode.OK);
        }
    }
}