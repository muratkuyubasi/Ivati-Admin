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
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllDebtorsPaginationQueryHandler : IRequestHandler<GetAllDebtorsPaginationQuery, ServiceResponse<FamilyDebtorPaginationDto>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;
        List<string> message = new List<string>();

        

        public GetAllDebtorsPaginationQueryHandler(IMapper mapper, IDebtorRepository debtorRepository, IFamilyRepository familyRepository)
        {
            _debtorRepository = debtorRepository;
            _familyRepository = familyRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<FamilyDebtorPaginationDto>> Handle(GetAllDebtorsPaginationQuery request, CancellationToken cancellationToken)
        {
            message.Add("Veri Bulunamadı!");
            var family = _familyRepository.All.Include(x => x.Debtors).ThenInclude(x=>x.DebtorType).Include(x=>x.FamilyMembers).ThenInclude(x=>x.MemberUser).AsQueryable();

            if (!request.Search.Equals("0"))
            {
                family = family.Where(x => x.MemberId.ToString().StartsWith(request.Search.ToString()) 
                || x.Debtors.Any(X=>X.DebtorNumber.StartsWith(request.Search.ToString()) && X.IsPayment == request.IsPayment) 
                || x.FamilyMembers.Any(x=>x.MemberUser.FirstName.Contains(request.Search))
                || x.FamilyMembers.Any(x=>x.MemberUser.LastName.Contains(request.Search)));
            }
            if (request.Year != 0)
            {
                int a = (int)request.Year;
                family = family.Where(x => x.Debtors.Any(y => y.CreationDate.Date.ToString().StartsWith(a.ToString())));
            }
            if (request.IsPayment != null)
            {
                family = family.Where(x => x.Debtors.Any(x => x.IsPayment == request.IsPayment));
            }

            var paginationData = await family.Skip(request.Skip * request.PageSize).Take(request.PageSize).ToListAsync();

                var list = new FamilyDebtorPaginationDto
                {
                    Data = _mapper.Map<List<FamilyInformationDTO>>(paginationData),
                    Skip = request.Skip,
                    TotalCount = family.Count(),
                    PageSize = request.PageSize,
                };
                return ServiceResponse<FamilyDebtorPaginationDto>.ReturnResultWith200(list);
        }
    }
}
