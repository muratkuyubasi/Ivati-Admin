using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PT.MediatR.Commands
{
    public class AddFrontGalleryRecordCommand : IRequest<ServiceResponse<FrontGalleryRecordDto>>
    {
        public int FrontGalleryId { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(750)]
        public string Slug { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
