using MediatR;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Commands
{
    public class AddLogCommand : IRequest<ServiceResponse<NLogDto>>
    {
        public string ErrorMessage { get; set; }
        public string Stack { get; set; }
    }
}
