using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
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
    public class GetHacPilgrimCandidateQueryHandler : IRequestHandler<GetHacPilgrimCandidateQuery, ServiceResponse<HacPaginationDto>>
    {
        private readonly IHacRepository _hacRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _unitOfWork;

        public GetHacPilgrimCandidateQueryHandler(IMapper mapper, IHacRepository hacRepository, IUnitOfWork<PTContext> uow)
        {
            _mapper = mapper;
            _hacRepository = hacRepository;
            _unitOfWork = uow;
        }
        public async Task<ServiceResponse<HacPaginationDto>> Handle(GetHacPilgrimCandidateQuery request, CancellationToken cancellationToken)
        {
            var candidate = _hacRepository.AllIncluding(g => g.Gender, m => m.MaritalStatus, c => c.ClosestAssociation, rt => rt.RoomType, d => d.DepartureAirport, l => l.LandingAirport, p=> p.Period).AsQueryable();

            if (request.Search != null)
            {
                candidate = candidate.Where(x => x.ClosestAssociation.Name.Contains(request.Search)
                || x.MaritalStatus.Name.Contains(request.Search)
                || x.Gender.Name.Contains(request.Search)
                || x.Name.Contains(request.Search) || x.Surname.Contains(request.Search) || x.PhoneNumber.Contains(request.Search) || x.Nationality.Contains(request.Search) || x.TurkeyIdentificationNumber.Contains(request.Search) || x.DateOfBirth.Year.ToString().Contains(request.Search) || x.Period.Name.Contains(request.Search));
            }
            if (request.PeriodId != 0)
            {
                candidate = candidate.Where(x => x.PeriodId == request.PeriodId);
            }

            var paginationData = await candidate.Skip(request.Skip * request.PageSize).Take(request.PageSize).ToListAsync();

            var list = new HacPaginationDto
            {
                Data = _mapper.Map<List<HacFormDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = candidate.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<HacPaginationDto>.ReturnResultWith200(list);
        }
    }
}
