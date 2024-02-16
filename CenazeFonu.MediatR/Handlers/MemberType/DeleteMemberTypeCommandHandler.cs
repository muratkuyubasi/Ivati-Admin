using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeleteMemberTypeCommandHandler : IRequestHandler<DeleteMemberTypeCommand, ServiceResponse<MemberTypeDTO>>
    {
        private readonly IMemberTypeRepository _memberTypeRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteMemberTypeCommandHandler(IMemberTypeRepository memberTypeRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _memberTypeRepository = memberTypeRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<MemberTypeDTO>> Handle(DeleteMemberTypeCommand request, CancellationToken cancellationToken)
        {
            var data = _memberTypeRepository.FindBy(x=>x.Id == request.Id).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<MemberTypeDTO>.Return409("Bu ID'ye ait bir kullanıcı tipi bulunmamaktadır!");
            }
            _memberTypeRepository.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<MemberTypeDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<MemberTypeDTO>.ReturnResultWith200(_mapper.Map<MemberTypeDTO>(data));
        }
    }
}
