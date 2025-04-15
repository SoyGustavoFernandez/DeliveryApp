using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;

namespace RoleService.Application.Commands.Permisos.Handler
{
    public class DeletePermisoHandler(IPermisoRepository repository) : IRequestHandler<DeletePermisoCommand, Response<string>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<string>> Handle(DeletePermisoCommand request, CancellationToken cancellationToken)
        {
            var permiso = await _repository.GetPermisoByIdAsync(request.Id);

            if (permiso == null)
            {
                return new Response<string>(false, "Permiso no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }
            
            permiso.Vigente = false;
            permiso.FecMod = DateTime.UtcNow;

            await _repository.UpdatePermisoAsync(permiso);

            return new Response<string>(true, "Permiso eliminado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}