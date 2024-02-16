using AutoMapper;
using AutoMapper.Execution;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
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
    public class TransferChildrenToAnotherFamilyCommandHandler : IRequestHandler<TransferChildrenToAnotherFamilyCommand, ServiceResponse<List<FamilyMemberDTO>>>
    {
        private readonly IFamilyMemberRepository _memberRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IFamilyRepository _familyRepository;

        public TransferChildrenToAnotherFamilyCommandHandler(IFamilyMemberRepository memberRepository, IMapper mapper, IUnitOfWork<PTContext> uow, IFamilyRepository familyRepository)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
            _uow = uow;
            _familyRepository = familyRepository;
        }

        public async Task<ServiceResponse<List<FamilyMemberDTO>>> Handle(TransferChildrenToAnotherFamilyCommand request, CancellationToken cancellationToken)
        {
            var members = _memberRepository.All.Include(x=>x.MemberUser).Where(x=>x.FamilyId == request.FamilyId && x.MemberTypeId == 3).ToList();
            foreach (var member in members)
            {
                member.FamilyId = request.TransferFamilyId;
                _memberRepository.Update(member);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse< List < FamilyMemberDTO >>.Return409("İşlem sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse< List < FamilyMemberDTO >>.ReturnResultWith200(_mapper.Map< List < FamilyMemberDTO >> (members));
        }
    }
}
