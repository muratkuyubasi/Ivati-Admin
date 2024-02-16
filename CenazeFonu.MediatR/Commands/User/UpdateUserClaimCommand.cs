using CenazeFonu.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class UpdateUserClaimCommand : IRequest<ServiceResponse<UserClaimDto>>
    {
        public Guid Id { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }
}
