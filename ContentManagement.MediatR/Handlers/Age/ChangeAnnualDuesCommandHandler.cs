using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
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
    public class ChangeAnnualDuesCommandHandler : IRequestHandler<ChangeAnnualDuesCommand, ServiceResponse<List<AgeDTO>>>
    {
        private readonly IAgeRepository _ageRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;

        public ChangeAnnualDuesCommandHandler(IAgeRepository ageRepository, IUnitOfWork<PTContext> uow, IMapper mapper)
        {
            _ageRepository = ageRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<AgeDTO>>> Handle(ChangeAnnualDuesCommand request, CancellationToken cancellationToken)
        {
            var datas = _ageRepository.All.ToList();
            foreach (var data in datas)
            {
                data.Dues = request.Dues;
                data.Amount = 0;
                data.Amount = (decimal)(data.EntranceFree + data.Dues);
                _ageRepository.Update(data);
            }
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<List<AgeDTO>>.Return409("Güncelleme sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<List<AgeDTO>>.ReturnResultWith200(_mapper.Map<List<AgeDTO>>(datas));
        }
    }
}
