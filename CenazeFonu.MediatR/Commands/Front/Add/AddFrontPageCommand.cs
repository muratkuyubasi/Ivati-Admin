
using MediatR;
using System;
using System.Collections.Generic;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.Data;

namespace PT.MediatR.Commands
{
    public class AddFrontPageCommand : IRequest<ServiceResponse<FrontPageDto>>
    {
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        public List<FrontPageRecord> FrontPageRecords { get; set; }
    }
}
