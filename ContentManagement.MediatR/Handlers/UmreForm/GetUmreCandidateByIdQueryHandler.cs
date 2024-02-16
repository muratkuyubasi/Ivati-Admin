using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetUmreCandidateByIdQueryHandler : IRequestHandler<GetUmreCandidateByIdQuery, ServiceResponse<UmreFormDTO>>
    {
        private readonly IUmreFormRepository _formRepository;
        private readonly IMapper _mapper;

        public GetUmreCandidateByIdQueryHandler(IUmreFormRepository formRepository, IMapper mapper)
        {
            _formRepository = formRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UmreFormDTO>> Handle(GetUmreCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _formRepository.FindBy(x => x.Id == request.Id)
                .Include(x=>x.Gender)
                .Include(x=>x.RoomType)
                .Include(x=>x.MaritalStatus)
                .Include(x=>x.ClosestAssociation)
                .Include(x=>x.DepartureAirport)
                .Include(x=>x.LandingAirport)
                .Include(X=>X.Period)
                .FirstOrDefaultAsync();
            if (candidate == null) { return ServiceResponse<UmreFormDTO>.Return409("Böyle bir aday bulunmamaktadır!"); }
            return ServiceResponse<UmreFormDTO>.ReturnResultWith200(_mapper.Map<UmreFormDTO>(candidate));
        }
    }
}
