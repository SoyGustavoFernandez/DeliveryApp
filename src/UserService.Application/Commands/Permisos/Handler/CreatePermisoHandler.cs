using DeliveryApp.Common.Entities;
using MediatR;
using System.Net;
using UserService.Application.Interfaces;
using UserService.Domain.Entity;

namespace UserService.Application.Commands.Permisos.Handler
{
    public class CreatePermisoHandler(IPermisoRepository repository) : IRequestHandler<CreatePermisoCommand, Response<string>>
    {
        private readonly IPermisoRepository _repository = repository;

        public async Task<Response<string>> Handle(CreatePermisoCommand request, CancellationToken cancellationToken)
        {
            Permiso permiso = new()
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            await _repository.AddPermisoAsync(permiso);

            return new Response<string>(true, "Permiso registrado exitosamente", permiso.Id.ToString(), (int)HttpStatusCode.Created);
        }
    }
}