
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using MediatR;
namespace ContentManagement.MediatR.Queries
{
    public class GetFrontGalleryMediaQuery : IRequest<ServiceResponse<FrontGalleryMediaDto>>
    {
        public int Id { get; set; }
    }
}
