using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Application.Interfaces;
using System.Net;

namespace RoleService.Application.Commands.Role.Handler
{
    public class UpdateRoleHandler(IRoleRepository repository, IPermisoRepository permisoRepository) : IRequestHandler<UpdateRoleCommand, Response<string>>
    {
        private readonly IRoleRepository _repository = repository;

        private readonly IPermisoRepository _permisoRepository = permisoRepository;

        public async Task<Response<string>> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var rol = await _repository.GetRoleByIdAsync(request.Id, incluirRelaciones: true);

            if (rol == null)
            {
                return new Response<string>(false, "Rol no encontrado", string.Empty, (int)HttpStatusCode.NotFound);
            }
            
            rol.Nombre = request.Nombre;
            rol.Descripcion = request.Descripcion;
            rol.FecMod = DateTime.UtcNow;

            if (rol.RolPermisos.Count > 0)
            {
                await _repository.RemoveRangePermisos(rol.RolPermisos);
            }

            if (request.Permisos != null && request.Permisos.Count > 0)
            {
                var permisosValidos = await _permisoRepository.GetPermisosByIdsAsync(request.Permisos);

                if (permisosValidos.Count != request.Permisos.Count)
                {
                    return new Response<string>(false, "Algunos permisos no existen", string.Empty, (int)HttpStatusCode.BadRequest);
                }

                await _repository.UpdatePermisosAsync(rol, permisosValidos);
            }

            await _repository.UpdateRoleAsync(rol);

            return new Response<string>(true, "Rol actualizado exitosamente", rol.Id.ToString(), (int)HttpStatusCode.OK);
        }
    }
}