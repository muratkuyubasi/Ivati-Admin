
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

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
