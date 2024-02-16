using CenazeFonu.Data.Dto;
using MediatR;
using System;
using CenazeFonu.Helper;
using System.Collections.Generic;
using CenazeFonu.Data;

namespace PT.MediatR.Commands
{
    public class UpdateFrontPageCommand : IRequest<ServiceResponse<FrontPageDto>>
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        public List<FrontPageRecord> FrontPageRecords { get; set; }
    }
}
