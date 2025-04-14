using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using RoleService.Application.Interfaces;
using RoleService.Domain.Entity;

namespace RoleService.Application.Commands.Permisos.Handler
{
    public class CreatePermisoHandler(IPermisoRepository repository) : IRequestHandler<CreatePermisoCommand, Response<string>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<string>> Handle(CreatePermisoCommand request, CancellationToken cancellationToken)
        {
            Permiso permiso = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                IdUsuReg = Guid.Parse("4c86ff9d-fed2-43aa-ab6d-457525de1a88")
            };

            await _repository.AddPermisoAsync(permiso);

            return new Response<string>(true, "Permiso registrado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.Created);
        }
    }
}