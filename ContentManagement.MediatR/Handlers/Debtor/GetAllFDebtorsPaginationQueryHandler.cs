using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using ContentManagement.MediatR.Queries;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Domain;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Data.Entity.Core.Objects;

namespace ContentManagement.MediatR.Handlers
{
    public class GetAllFDebtorsPaginationQueryHandler : IRequestHandler<GetAllFDebtorsPaginationQuery, ServiceResponse<DebtorPaginationDto>>
    {
        private readonly IDebtorRepository _debtorRepository;
        private readonly IMapper _mapper;

        public GetAllFDebtorsPaginationQueryHandler(IDebtorRepository debtorRepository, IMapper mapper)
        {
            _debtorRepository = debtorRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<DebtorPaginationDto>> Handle(GetAllFDebtorsPaginationQuery request, CancellationToken cancellationToken)
        {
            var debtors = _debtorRepository.All
                .Include(x => x.Family)
                .Include(x => x.DebtorType)
                .Include(x => x.Family.FamilyMembers)
                .ThenInclude(x => x.MemberUser)
                .Select(y => new DebtorSimpleDTO
                {
                    Id = y.Id,
                    FamilyId = y.Family.Id,
                    FullName = y.Family.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.FirstName + " " + y.Family.FamilyMembers.Where(x => x.MemberTypeId == 1).FirstOrDefault().MemberUser.LastName,
                    CreationDate = y.CreationDate.Day.ToString() + "." + y.CreationDate.Month.ToString() + "." + y.CreationDate.Year.ToString(),
                    DebtorNumber = y.DebtorNumber,
                    MemberId = y.Family.MemberId,
                    DueDate = y.DueDate.Value.Day.ToString() + "." + y.DueDate.Value.Month.ToString() + "." + y.DueDate.Value.Year.ToString(),
                    PaymentDate = y.PaymentDate.HasValue ? y.PaymentDate.Value.Day.ToString() + "." + y.PaymentDate.Value.Month.ToString() + "." + y.PaymentDate.Value.Year.ToString() : null,
                    Amount = y.Amount,
                    DebtorType = y.DebtorType.Name,
                    IsPayment = y.IsPayment,
                    CityId = y.Family.CityId
                }).AsNoTracking();

            if (request.Year != 0)
            {
                debtors = debtors.Where(x => x.CreationDate.Contains(request.Year.ToString()));
            }
            if (request.IsPayment != "0")
            {
                if (request.IsPayment == "1")
                {
                    debtors = debtors.Where(x => x.IsPayment == false);
                }
                if (request.IsPayment == "2")
                {
                    debtors = debtors.Where(x => x.IsPayment == true);
                }
            }

            if (request.CityId != 0)
            {
                debtors = debtors.Where(x => x.CityId == request.CityId);
            }

            if (request.OrderBy != null)
            {
                debtors = debtors.OrderBy(request.OrderBy);
            }
            else { debtors = debtors.OrderBy(X => X.MemberId); }

            if (request.Search != null)
            {
                debtors = debtors.Where(x => x.DebtorNumber.StartsWith(request.Search)
                        || x.MemberId.ToString().StartsWith(request.Search)
                        || x.FullName.StartsWith(request.Search)
                        || x.DebtorType.StartsWith(request.Search)
                        || x.FamilyId.ToString().StartsWith(request.Search)
                        || x.CreationDate.StartsWith(request.Search)
                        || x.DueDate.StartsWith(request.Search) || x.PaymentDate.StartsWith(request.Search));

                        /*|| x.CreationDate.ToString().StartsWith(request.Search) || x.DueDate.ToString().StartsWith(request.Search) || x.PaymentDate.ToString().StartsWith(request.Search)*/
                //debtors = from deb in debtors
                //          where deb.DebtorNumber == request.Search ||
                //                                    deb.MemberId.ToString().StartsWith(request.Search) ||
                //                                    deb.FullName.StartsWith(request.Search) ||
                //                                    deb.DebtorType.StartsWith(request.Search) ||
                //                                    deb.FamilyId.ToString().StartsWith(request.Search) ||
                //                                    deb.CreationDate.StartsWith(request.Search) ||
                //                                    deb.DueDate.StartsWith(request.Search) || deb.PaymentDate.StartsWith(request.Search)
                //          select deb;
            }

            var paginationData = debtors.AsEnumerable().Skip(request.Skip * request.PageSize).Take(request.PageSize).ToList();
            //var paginationData = debtors.AsEnumerable().Take((request.Skip * request.PageSize)..request.PageSize).ToList();

            var list = new DebtorPaginationDto
            {
                Data = _mapper.Map<List<DebtorSimpleDTO>>(paginationData),
                Skip = request.Skip,
                TotalCount = debtors.Count(),
                PageSize = request.PageSize,
            };
            return ServiceResponse<DebtorPaginationDto>.ReturnResultWith200(list);
        }
    }
}
