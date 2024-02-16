using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;
using System.Collections.Generic;
using CenazeFonu.Data;

namespace PT.MediatR.Commands
{
    public class UpdateFrontMenuCommand : IRequest<ServiceResponse<FrontMenuDto>>
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> FrontPageId { get; set; }
        public List<FrontMenuRecord> FrontMenuRecords { get; set; }
    }
}
