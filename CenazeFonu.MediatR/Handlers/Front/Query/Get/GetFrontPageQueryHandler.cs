using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;
using Microsoft.EntityFrameworkCore;

namespace CenazeFonu.Mediatr.Handlers
{
    public class GetFrontPageQueryHandler : IRequestHandler<GetFrontPageQuery, ServiceResponse<FrontPageDto>>
    {
        private readonly IFrontPageRepository _FrontPageRepository;
        private readonly IMapper _mapper;

        public GetFrontPageQueryHandler(
           IFrontPageRepository FrontPageRepository,
           IMapper mapper)
        {
            _FrontPageRepository = FrontPageRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontPageDto>> Handle(GetFrontPageQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontPageRepository.FindByInclude(x => x.Code == request.Code,i=>i.FrontPageRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontPageDto>.ReturnResultWith200(_mapper.Map<FrontPageDto>(entity));
            else
                return ServiceResponse<FrontPageDto>.Return404();
        }
    }
}
