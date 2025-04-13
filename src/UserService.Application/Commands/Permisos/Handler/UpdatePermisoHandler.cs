using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using UserService.Application.Interfaces;

namespace UserService.Application.Commands.Permisos.Handler
{
    public class UpdatePermisoHandler(IPermisoRepository repository) : IRequestHandler<UpdatePermisoCommand, Response<string>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<string>> Handle(UpdatePermisoCommand request, CancellationToken cancellationToken)
        {
            var permiso = await _repository.GetPermisoByIdAsync(request.Id);
            if (permiso == null)
            {
                return new Response<string>(false, "Permiso no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }

            permiso.Nombre = request.Nombre;
            permiso.Descripcion = request.Descripcion;
            permiso.FecMod = DateTime.UtcNow;

            await _repository.UpdatePermisoAsync(permiso);
            
            return new Response<string>(true, "Permiso actualizado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}