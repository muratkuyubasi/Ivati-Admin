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
    public class UpdateMemberTypeCommandHandler : IRequestHandler<UpdateMemberTypeCommand, ServiceResponse<MemberTypeDTO>>
    {
        private readonly IMemberTypeRepository _memberTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateMemberTypeCommandHandler(IMemberTypeRepository memberTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _memberTypeRepository = memberTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<MemberTypeDTO>> Handle(UpdateMemberTypeCommand request, CancellationToken cancellationToken)
        {
            var data = _memberTypeRepository.FindBy(X => X.Id == request.Id).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<MemberTypeDTO>.Return409("Bu ID'ye ait bir kullanıcı tipi bulunmamaktadır!");
            }
            data.Name = request.Name;
            _memberTypeRepository.Update(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<MemberTypeDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<MemberTypeDTO>.ReturnResultWith200(_mapper.Map<MemberTypeDTO>(data));
        }
    }
}
