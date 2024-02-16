using MediatR;
using System;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Queries
{
    public class GetAppSettingQuery : IRequest<ServiceResponse<AppSettingDto>>
    {
        public Guid Id { get; set; }
    }
}
