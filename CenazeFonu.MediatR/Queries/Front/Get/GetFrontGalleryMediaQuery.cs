
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using MediatR;
namespace CenazeFonu.MediatR.Queries
{
    public class GetFrontGalleryMediaQuery : IRequest<ServiceResponse<FrontGalleryMediaDto>>
    {
        public int Id { get; set; }
    }
}
