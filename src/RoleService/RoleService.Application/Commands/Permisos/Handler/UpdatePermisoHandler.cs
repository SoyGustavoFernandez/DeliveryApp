using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;

namespace RoleService.Application.Commands.Permisos.Handler
{
    public class UpdatePermisoHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdatePermisoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(UpdatePermisoCommand request, CancellationToken cancellationToken)
        {
            var permiso = await _unitOfWork.PermisoRepository.GetPermisoByIdAsync(request.Id);

            if (permiso == null)
            {
                return new Response<string>(false, "Permiso no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }

            var permisoExistente = await _unitOfWork.PermisoRepository.GetPermisoByNombreAsync(request.Nombre);

            if (permisoExistente != null && permisoExistente.Id != permiso.Id)
            {
                return new Response<string>(false, "Ya existe un permiso con el mismo nombre", string.Empty, (int)HttpStatusCode.BadRequest);
            }

            permiso.Nombre = request.Nombre;
            permiso.Descripcion = request.Descripcion;
            permiso.FecMod = DateTime.UtcNow;
            permiso.IdUsuMod = Guid.NewGuid();

            await _unitOfWork.PermisoRepository.UpdatePermisoAsync(permiso);
            await _unitOfWork.CompleteAsync();

            return new Response<string>(true, "Permiso actualizado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}