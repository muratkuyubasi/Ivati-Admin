﻿using CenazeFonu.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class UpdateUserRoleCommand : IRequest<ServiceResponse<UserRoleDto>>
    {
        public Guid Id { get; set; }
        public List<UserRoleDto> UserRoles { get; set; }
    }
}
