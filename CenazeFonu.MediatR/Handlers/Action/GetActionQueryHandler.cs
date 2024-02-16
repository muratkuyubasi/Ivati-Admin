using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetActionQueryHandler : IRequestHandler<GetActionQuery, ServiceResponse<ActionDto>>
    {
        private readonly IActionRepository _actionRepository;
        private readonly IMapper _mapper;

        public GetActionQueryHandler(
           IActionRepository actionRepository,
           IMapper mapper)
        {
            _actionRepository = actionRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ActionDto>> Handle(GetActionQuery request, CancellationToken cancellationToken)
        {
            var entity = await _actionRepository.FindAsync(request.Id);
            if (entity != null)
                return ServiceResponse<ActionDto>.ReturnResultWith200(_mapper.Map<ActionDto>(entity));
            else
                return ServiceResponse<ActionDto>.Return404();
        }
    }
}
