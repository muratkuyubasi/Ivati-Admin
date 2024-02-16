using ContentManagement.Data.Dto;
using MediatR;
using System;
using ContentManagement.Helper;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PT.MediatR.Commands
{
    public class UpdateFrontPageRecordCommand : IRequest<ServiceResponse<FrontPageRecordDto>>
    {
        public int Id { get; set; }
        public Guid Code { get; set; }
        public int FrontPageId { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }
        public string PageContent { get; set; }
        public string LanguageCode { get; set; }
        public string FileUrl { get; set; }
    }
}
