using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;
using System.Collections.Generic;
using ContentManagement.Data;

namespace PT.MediatR.Commands
{
    public class UpdateFrontAnnouncementCommand : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public short Order { get; set; }
        public bool IsSlider { get; set; }
        public bool IsNews { get; set; }
        public bool IsAnnouncement { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        public List<FrontAnnouncementRecord> FrontAnnouncementRecords { get; set; }
    }
}
