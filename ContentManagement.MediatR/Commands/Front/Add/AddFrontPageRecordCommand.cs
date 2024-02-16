
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace PT.MediatR.Commands
{
    public class AddFrontPageRecordCommand : IRequest<ServiceResponse<FrontPageRecordDto>>
    {
        public int FrontPageId { get; set; }

        [MaxLength(1000)]
        public string Name { get; set; }
        public string PageContent { get; set; }
        public string LanguageCode { get; set; }
        public string FileUrl { get; set; }
    }
}
