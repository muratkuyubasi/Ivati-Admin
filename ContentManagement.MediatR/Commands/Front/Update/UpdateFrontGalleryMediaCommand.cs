using MediatR;
using Microsoft.AspNetCore.Http;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;

namespace PT.MediatR.Commands
{
    public class UpdateFrontGalleryMediaCommand : IRequest<ServiceResponse<FrontGalleryMediaDto>>
    {
        public int Id { get; set; }
        public int FrontGalleryRecordId { get; set; }
        public string Title { get; set; }
        public string FileUrl { get; set; }
        public short Order { get; set; }
        public bool IsActive { get; set; }
        public IFormFile FormFile { get; set; }
    }
}
