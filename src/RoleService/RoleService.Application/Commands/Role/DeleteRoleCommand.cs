﻿using DeliveryApp.Common.Entities;
using MediatR;

namespace RoleService.Application.Commands.Role
{
    public class DeleteRoleCommand(Guid id) : IRequest<Response<string>>
    {
        /// <summary>
        /// Identificador del registro
        /// </summary>
        /// <remarks>
        /// Constructor
        /// </remarks>
        /// <param name="id">Identificador del registro.</param>
        public Guid Id { get; set; } = id;
    }
}