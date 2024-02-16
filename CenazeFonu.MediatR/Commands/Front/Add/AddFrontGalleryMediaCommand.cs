using MediatR;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using CenazeFonu.Helper;
using CenazeFonu.Data.Dto;

namespace PT.MediatR.Commands
{
    public class AddFrontGalleryMediaCommand : IRequest<ServiceResponse<FrontGalleryMediaDto>>
    {
        public int FrontGalleryRecordId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
