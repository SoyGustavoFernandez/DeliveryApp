using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;
using RoleService.Domain.Entity;

namespace RoleService.Application.Commands.Permisos.Handler
{
    public class CreatePermisoHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreatePermisoCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreatePermisoCommand request, CancellationToken cancellationToken)
        {
            var permisoExistente = await _unitOfWork.PermisoRepository.GetPermisoByNombreAsync(request.Nombre);

            if (permisoExistente != null)
            {
                return new Response<string>(false, "Ya existe un permiso con el mismo nombre", string.Empty, (int)HttpStatusCode.BadRequest);
            }

            Permiso permiso = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                IdUsuReg = Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88")
            };

            await _unitOfWork.PermisoRepository.AddPermisoAsync(permiso);
            await _unitOfWork.CompleteAsync(); 

            return new Response<string>(true, "Permiso registrado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.Created);
        }
    }
}