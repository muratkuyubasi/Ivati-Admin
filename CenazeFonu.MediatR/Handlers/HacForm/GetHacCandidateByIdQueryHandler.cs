using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetHacCandidateByIdQueryHandler : IRequestHandler<GetHacCandidateByIdQuery, ServiceResponse<HacFormDTO>>
    {
        private readonly IHacRepository _hacRepository;
        private readonly IMapper _mapper;

        public GetHacCandidateByIdQueryHandler(IHacRepository hacRepository, IMapper mapper)
        {
            _hacRepository = hacRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<HacFormDTO>> Handle(GetHacCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _hacRepository.FindBy(x => x.Id == request.Id).Include(x => x.Gender).Include(x => x.RoomType)
                .Include(x => x.MaritalStatus).Include(x => x.ClosestAssociation)
                .Include(x=>x.DepartureAirport)
                .Include(x=>x.LandingAirport)
                .Include(x=>x.Period).FirstOrDefaultAsync();
            if (candidate == null)
            {
                return ServiceResponse<HacFormDTO>.Return409("Böyle bir aday bulunmamaktadır!");
            }
            return ServiceResponse<HacFormDTO>.ReturnResultWith200(_mapper.Map<HacFormDTO>(candidate));
        }
    }
}
