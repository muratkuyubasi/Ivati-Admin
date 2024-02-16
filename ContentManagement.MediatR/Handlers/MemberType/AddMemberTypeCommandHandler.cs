using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class AddMemberTypeCommandHandler : IRequestHandler<AddMemberTypeCommand, ServiceResponse<MemberTypeDTO>>
    {
        private readonly IMemberTypeRepository _memberTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddMemberTypeCommandHandler(IMemberTypeRepository memberTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _memberTypeRepository = memberTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<MemberTypeDTO>> Handle(AddMemberTypeCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<MemberType>(request);
            _memberTypeRepository.Add(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<MemberTypeDTO>.Return409("Oluşturma işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<MemberTypeDTO>.ReturnResultWith200(_mapper.Map<MemberTypeDTO>(model));
        }
    }
}
