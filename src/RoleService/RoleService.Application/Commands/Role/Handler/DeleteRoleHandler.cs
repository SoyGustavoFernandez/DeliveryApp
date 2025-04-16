using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Application.Interfaces;
using System.Net;

namespace RoleService.Application.Commands.Role.Handler
{
    public class DeleteRoleHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await unitOfWork.RoleRepository.GetRoleByIdAsync(request.Id, incluirRelaciones: true);

            if (role == null)
            {
                return new Response<string>(false, "Role no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }

            role.Vigente = false;
            role.FecMod = DateTime.UtcNow;

            if (role.RolPermisos.Count != 0)
            {
                await unitOfWork.RoleRepository.RemoveRangePermisos(role.RolPermisos);
            }

            await unitOfWork.RoleRepository.UpdateRoleAsync(role);
            await unitOfWork.CompleteAsync();

            return new Response<string>(true, "Role eliminado exitosamente", role.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}