using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PT.MediatR.Commands
{
    public class UpdateFrontGalleryRecordCommand : IRequest<ServiceResponse<FrontGalleryRecordDto>>
    {
        public int Id { get; set; }
        public int FrontGalleryId { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(750)]
        public string Slug { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
