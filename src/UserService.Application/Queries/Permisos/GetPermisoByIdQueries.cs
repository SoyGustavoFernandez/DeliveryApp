﻿using DeliveryApp.Common.Entities;
using MediatR;
using UserService.Domain.Entity;

namespace UserService.Application.Queries.Permisos
{
    /// <summary>
    /// Query para obtener un permiso por su identificador.
    /// </summary>
    /// <remarks>
    /// Constructor.
    /// </remarks>
    /// <param name="id">Identificador del registro</param>
    public class GetPermisoByIdQueries(Guid id) : IRequest<Response<Permiso>>
    {
        /// <summary>
        /// Identificador del permiso.
        /// </summary>
        public Guid Id { get; set; } = id;
    }
}