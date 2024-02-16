
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace PT.MediatR.Commands
{
    public class AddFrontMenuRecordCommand : IRequest<ServiceResponse<FrontMenuRecordDto>>
    {
        public int FrontMenuId { get; set; }
        [MaxLength(500)]
        public string Name { get; set; }
        [MaxLength(750)]
        public string Slug { get; set; }
        [MaxLength(7)]
        public string LanguageCode { get; set; }
    }
}
