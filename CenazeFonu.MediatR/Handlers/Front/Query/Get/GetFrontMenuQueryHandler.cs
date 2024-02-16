using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CenazeFonu.Helper;

namespace CenazeFonu.Mediatr.Handlers
{
    public class GetFrontMenuQueryHandler : IRequestHandler<GetFrontMenuQuery, ServiceResponse<FrontMenuDto>>
    {
        private readonly IFrontMenuRepository _FrontMenuRepository;
        private readonly IMapper _mapper;

        public GetFrontMenuQueryHandler(
           IFrontMenuRepository FrontMenuRepository,
           IMapper mapper)
        {
            _FrontMenuRepository = FrontMenuRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontMenuDto>> Handle(GetFrontMenuQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontMenuRepository.FindByInclude(x => x.Code == request.Code, i=>i.FrontMenuRecords).Include(x=>x.FrontPage).ThenInclude(x=>x.FrontPageRecords).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontMenuDto>.ReturnResultWith200(_mapper.Map<FrontMenuDto>(entity));
            else
                return ServiceResponse<FrontMenuDto>.Return404();
        }
    }
}
