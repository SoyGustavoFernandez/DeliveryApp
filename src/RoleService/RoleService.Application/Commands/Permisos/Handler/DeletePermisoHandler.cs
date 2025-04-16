using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;

namespace RoleService.Application.Commands.Permisos.Handler
{
    public class DeletePermisoHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeletePermisoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(DeletePermisoCommand request, CancellationToken cancellationToken)
        {
            var permiso = await _unitOfWork.PermisoRepository.GetPermisoByIdAsync(request.Id);

            if (permiso == null)
            {
                return new Response<string>(false, "Permiso no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }
            
            permiso.Vigente = false;
            permiso.FecMod = DateTime.UtcNow;

            await _unitOfWork.PermisoRepository.UpdatePermisoAsync(permiso);
            await _unitOfWork.CompleteAsync();

            return new Response<string>(true, "Permiso eliminado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}