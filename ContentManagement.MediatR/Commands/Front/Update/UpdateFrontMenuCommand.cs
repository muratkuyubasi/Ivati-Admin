using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;
using System.Collections.Generic;
using ContentManagement.Data;

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
