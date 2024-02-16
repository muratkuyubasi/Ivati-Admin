using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class GetUnpaidDuesByYearQueryHandler : IRequestHandler<GetUnpaidDuesByYearQuery, ServiceResponse<List<DebtorDTO>>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public GetUnpaidDuesByYearQueryHandler(IDebtorRepository debtorRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<List<DebtorDTO>>> Handle(GetUnpaidDuesByYearQuery request, CancellationToken cancellationToken)
        {
            var debtorsWithFamilyNames = await _debtorRepository.All.Where(x => x.CreationDate.Year.ToString() == request.Year && x.IsPayment == false).ToListAsync();
            return ServiceResponse<List<DebtorDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorDTO>>(debtorsWithFamilyNames));

            //var debtors = await _uow.Context.Debtors.Include(x=>x.Family)
            //    .ToListAsync();
            //return ServiceResponse<List<DebtorDTO>>.ReturnResultWith200(_mapper.Map<List<DebtorDTO>>(debtors));
        }
        
    }
}
