using MediatR;
using System;
using System.Collections.Generic;
using ContentManagement.Helper;
using ContentManagement.Data.Dto;

namespace PT.MediatR.Commands
{
    public class AddFrontMenuCommand : IRequest<ServiceResponse<FrontMenuDto>>
    {
        public short Order { get; set; }
        public string Url { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> ParentId { get; set; }
        public Nullable<int> FrontPageId { get; set; }
        public List<FrontMenuRecordDto> FrontMenuRecords { get; set; }
    }
}
