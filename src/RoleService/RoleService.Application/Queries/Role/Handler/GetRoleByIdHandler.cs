using DeliveryApp.Common.Entities;
using MediatR;
using RoleService.Application.Interfaces;
using RoleService.Domain.DTOs.Role;
using RoleService.Domain.Enums;
using System.Net;

namespace RoleService.Application.Queries.Role.Handler
{
    public class GetRoleByIdHandler(IRoleRepository repository) : IRequestHandler<GetRoleByIdQueries, Response<RoleResponseDTO>>
    {
        private readonly IRoleRepository _repository = repository;

        public async Task<Response<RoleResponseDTO>> Handle(GetRoleByIdQueries request, CancellationToken cancellationToken)
        {
            var rol = await _repository.GetRoleByIdAsync(request.Id, incluirRelaciones: request.IncluirDetalles);

            if (rol == null)
            {
                return new Response<RoleResponseDTO>(false, "Rol no encontrado", null!, (int)HttpStatusCode.NotFound);
            }

            var rolDto = new RoleResponseDTO
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
            };

            return new Response<RoleResponseDTO>(true, "Rol encontrado", rolDto, (int)HttpStatusCode.OK);
        }
    }
}