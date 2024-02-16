using ContentManagement.Data.Dto;
using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;

namespace ContentManagement.MediatR.Commands
{
    public class UpdateUserClaimCommand : IRequest<ServiceResponse<UserClaimDto>>
    {
        public Guid Id { get; set; }
        public List<UserClaimDto> UserClaims { get; set; }
    }
}
