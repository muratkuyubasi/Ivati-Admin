using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CenazeFonu.MediatR.Commands
{
    public class AddFrontAnnouncementRecordCommand : IRequest<ServiceResponse<FrontAnnouncementRecordDto>>
    {
        public int FrontAnnouncementId { get; set; }
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string FileUrl { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
