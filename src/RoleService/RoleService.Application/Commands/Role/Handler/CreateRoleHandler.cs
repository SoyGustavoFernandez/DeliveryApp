using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Application.Interfaces;
using RoleService.Domain.Entity;
using System.Net;

namespace RoleService.Application.Commands.Role.Handler
{
    public class CreateRoleHandlerr(IUnitOfWork unitOfWork) : IRequestHandler<CreateRoleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
        {
            var rol = new Rol
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                IdUsuReg = Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88")
            };

            await _unitOfWork.RoleRepository.AddRoleAsync(rol);

            if (request.Permisos != null && request.Permisos.Count != 0)
            {
                var permisosValidos = await _unitOfWork.PermisoRepository.GetPermisosByIdsAsync(request.Permisos);

                if (permisosValidos.Count != request.Permisos.Count)
                {
                    return new Response<string>(false, "Algunos permisos no existen", null!, (int)HttpStatusCode.NotFound);
                }

                await _unitOfWork.RoleRepository.AddPermisosAsync(rol, permisosValidos);
            }
            await _unitOfWork.CompleteAsync();

            return new Response<string>(true, "Rol registrado exitosamente", rol.Id.ToString(), (int)HttpStatusCode.Created);
        }
    }
}