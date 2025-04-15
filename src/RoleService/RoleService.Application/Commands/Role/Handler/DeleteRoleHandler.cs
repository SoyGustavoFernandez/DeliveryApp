using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Application.Interfaces;
using System.Net;

namespace RoleService.Application.Commands.Role.Handler
{
    public class DeleteRoleHandler(IRoleRepository repository) : IRequestHandler<DeleteRoleCommand, Response<string>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<string>> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetRoleByIdAsync(request.Id, incluirRelaciones: true);

            if (role == null)
            {
                return new Response<string>(false, "Role no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }

            role.Vigente = false;
            role.FecMod = DateTime.UtcNow;

            if (role.RolPermisos.Count != 0)
            {
                await _repository.RemoveRangePermisos(role.RolPermisos);
            }

            await _repository.UpdateRoleAsync(role);

            return new Response<string>(true, "Role eliminado exitosamente", role.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}