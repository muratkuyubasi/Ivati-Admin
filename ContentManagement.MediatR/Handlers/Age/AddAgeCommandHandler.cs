using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
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
    public class AddAgeCommandHandler : IRequestHandler<AddAgeCommand, ServiceResponse<AgeDTO>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddAgeCommandHandler(IAgeRepository ageRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _ageRepository = ageRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AgeDTO>> Handle(AddAgeCommand request, CancellationToken cancellationToken)
        {
            var age = _mapper.Map<Age>(request);
            var exists = _ageRepository.FindBy(x => x.MinAge == age.MinAge && x.MaxAge == age.MaxAge).FirstOrDefault();
            if (exists != null)
            {
                return ServiceResponse<AgeDTO>.Return409("Bu Yaş aralığına göre fiyatlandırma bulunmaktadır!");
            }
            if (age.Dues == 0 || age.Dues == null)
            {
                age.Dues = 350;
            }
            age.Amount = (decimal)(age.Dues + age.EntranceFree);
            _ageRepository.Add(age);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AgeDTO>.Return409("Ekleme işlemi sırasında bir problem oluştu!");
            }
            else return ServiceResponse<AgeDTO>.ReturnResultWith200(_mapper.Map<AgeDTO>(age));
        }
    }
}
