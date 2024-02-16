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
    public class GetPilgrimCandidateQueryHandler : IRequestHandler<GetPilgrimCandidateQuery, ServiceResponse<UmrePaginationDto>>
    {
        private readonly IUmreFormRepository _formRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _unitOfWork;

        public GetPilgrimCandidateQueryHandler(IMapper mapper, IUmreFormRepository umreFormRepository, IUnitOfWork<PTContext> uow)
        {
            _mapper = mapper;
            _formRepository = umreFormRepository;
            _unitOfWork = uow;
        }
        public async Task<ServiceResponse<UmrePaginationDto>> Handle(GetPilgrimCandidateQuery request, CancellationToken cancellationToken)
        {
            var candidate = _formRepository.AllIncluding(g => g.Gender, m => m.MaritalStatus, c => c.ClosestAssociation, rt => rt.RoomType, d => d.DepartureAirport, l => l.LandingAirport, p => p.Period).AsQueryable();

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

            var list = new UmrePaginationDto
            {
                Data = _mapper.Map<List<UmreFormDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = candidate.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<UmrePaginationDto>.ReturnResultWith200(list);
            //var candidate = _formRepository.AllIncluding(g => g.Gender, m => m.MaritalStatus, c => c.ClosestAssociation, rt => rt.RoomType, d => d.DepartureAirport, l => l.LandingAirport, p=>p.Period).AsQueryable();

            //if (request.Search != null)
            //{
            //    candidate = candidate.Where(x => x.ClosestAssociation.Name.Contains(request.Search)
            //    || x.MaritalStatus.Name.Contains(request.Search)
            //    || x.Gender.Name.Contains(request.Search)
            //    || x.Name.Contains(request.Search) || x.Surname.Contains(request.Search) || x.PhoneNumber.Contains(request.Search) || x.Nationality.Contains(request.Search) || x.TurkeyIdentificationNumber.Contains(request.Search) || x.DateOfBirth.Year.ToString().Contains(request.Search)
            //    || x.Period.Name.Contains(request.Search));
            //}
            //if (request.PeriodId != 0)
            //{
            //    candidate = candidate.Where(x => x.PeriodId == request.PeriodId);
            //}
            //var paginationData = await candidate.Skip(request.Skip * request.PageSize).Take(request.PageSize).ToListAsync();

            //var list = new UmrePaginationDto
            //{
            //    Data = _mapper.Map<List<UmreFormDTO>>(paginationData),
            //    Skip = request.Skip,
            //    TotalCount = candidate.Count(),
            //    PageSize = request.PageSize,
            //};
            //return ServiceResponse<UmrePaginationDto>.ReturnResultWith200(list);
        }
    }
}
