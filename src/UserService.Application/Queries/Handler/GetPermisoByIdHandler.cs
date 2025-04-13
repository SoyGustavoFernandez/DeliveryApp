using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using UserService.Application.Interfaces;
using UserService.Application.Queries.Permisos;
using UserService.Domain.Entity;

namespace UserService.Application.Queries.Handler
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