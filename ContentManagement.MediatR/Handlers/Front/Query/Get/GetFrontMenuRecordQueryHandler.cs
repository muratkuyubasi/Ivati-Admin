using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetFrontMenuRecordQueryHandler : IRequestHandler<GetFrontMenuRecordQuery, ServiceResponse<FrontMenuRecordDto>>
    {
        private readonly IFrontMenuRecordRepository _FrontMenuRecordRepository;
        private readonly IMapper _mapper;

        public GetFrontMenuRecordQueryHandler(
           IFrontMenuRecordRepository FrontMenuRecordRepository,
           IMapper mapper)
        {
            _FrontMenuRecordRepository = FrontMenuRecordRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FrontMenuRecordDto>> Handle(GetFrontMenuRecordQuery request, CancellationToken cancellationToken)
        {
            var entity = await _FrontMenuRecordRepository.FindBy( x=>x.Id == request.Id).FirstOrDefaultAsync();
            if (entity != null)
                return ServiceResponse<FrontMenuRecordDto>.ReturnResultWith200(_mapper.Map<FrontMenuRecordDto>(entity));
            else
                return ServiceResponse<FrontMenuRecordDto>.Return404();
        }
    }
}
