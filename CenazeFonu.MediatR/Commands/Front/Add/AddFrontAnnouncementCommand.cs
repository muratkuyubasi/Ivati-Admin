using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;

namespace CenazeFonu.MediatR.Commands
{
    public class AddFrontAnnouncementCommand : IRequest<ServiceResponse<FrontAnnouncementDto>>
    {
        public short Order { get; set; }
        public bool IsSlider { get; set; }
        public bool IsNews { get; set; }
        public bool IsAnnouncement { get; set; }
        public bool IsActive { get; set; }
        public Nullable<int> FrontGalleryId { get; set; }
        public List<FrontAnnouncementRecord> FrontAnnouncementRecords { get; set; }
    }
}
