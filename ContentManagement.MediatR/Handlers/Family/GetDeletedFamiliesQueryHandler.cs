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
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetDeletedFamiliesQueryHandler : IRequestHandler<GetDeletedFamiliesQuery, ServiceResponse<DeletedFamiliesPaginationDTO>>
    {
        private readonly IFamilyRepository _familyRepository;
        private readonly IMapper _mapper;

        public GetDeletedFamiliesQueryHandler(IFamilyRepository familyRepository, IMapper mapper)
        {
            _familyRepository = familyRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<DeletedFamiliesPaginationDTO>> Handle(GetDeletedFamiliesQuery request, CancellationToken cancellationToken)
        {
            var deletedFamilies = _familyRepository.All.Include(x => x.Address).Include(x => x.FamilyMembers).Where(x => x.IsDeleted == true);
            if (request.Search != null)
            {
                deletedFamilies = deletedFamilies.Where(x => x.Name.ToUpper().StartsWith(request.Search.ToUpper()) || x.MemberId.ToString().StartsWith(request.Search));
            }
            if (request.OrderBy != null)
            {
                deletedFamilies = deletedFamilies.OrderBy(request.OrderBy);
            }
            else { deletedFamilies = deletedFamilies.OrderBy(x => x.MemberId); }
            var paginationData = deletedFamilies.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).OrderBy(x => x.MemberId).ToList();

            var list = new DeletedFamiliesPaginationDTO
            {
                Data = _mapper.Map<List<DeletedFamiliesInformationDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = deletedFamilies.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<DeletedFamiliesPaginationDTO>.ReturnResultWith200(_mapper.Map<DeletedFamiliesPaginationDTO>(list));
        }
    }
}
