using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Application.Interfaces;
using RoleService.Domain.DTOs.Role;
using RoleService.Domain.Enums;
using System.Net;

namespace RoleService.Application.Queries.Role.Handler
{
    public class GetRolesHandler(IRoleRepository repository) : IRequestHandler<GetRolesQueries, Response<List<RoleResponseDTO>>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<List<RoleResponseDTO>>> Handle(GetRolesQueries request, CancellationToken cancellationToken)
        {
            var roles = await _repository.GetAllRolesAsync(request.IncluirDetalles);

            if (roles.Count == 0)
            {
                return new Response<List<RoleResponseDTO>>(false, "No se encontraron roles", null!, (int)HttpStatusCode.NotFound);
            }

            var rolesDto = roles.Select(rol => new RoleResponseDTO
            {
                Id = rol.Id,
                Nombre = rol.Nombre,
                Descripcion = rol.Descripcion,
                Permisos = rol.RolPermisos?
                    .Select(rp => new PermisoSimplificado
                    {
                        Id = rp.Permiso.Id,
                        Nombre = rp.Permiso.Nombre
                    })
                    .ToList() ?? [],
                TotalUsuarios = rol.UsuarioRoles?.Count ?? 0
            }).ToList();

            return new Response<List<RoleResponseDTO>>(true, "Roles encontrados", rolesDto, (int)HttpStatusCode.OK);
        }
    }
}