using AutoMapper;
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
    public class UpdateAgeCommandHandler : IRequestHandler<UpdateAgeCommand, ServiceResponse<AgeDTO>>
    {
        private readonly IAgeRepository _ageRepo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateAgeCommandHandler(IAgeRepository ageRepo, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _ageRepo = ageRepo;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AgeDTO>> Handle(UpdateAgeCommand request, CancellationToken cancellationToken)
        {
            var age = _ageRepo.FindBy(x => x.Id == request.Id).FirstOrDefault();
            age.Amount = request.Amount;
            age.MaxAge = request.MaxAge;
            age.MinAge = request.MinAge;
            age.EntranceFree = request.EntranceFree;
            age.Dues = request.Dues;
            _ageRepo.Update(age);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<AgeDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<AgeDTO>.ReturnResultWith200(_mapper.Map<AgeDTO>(age));
        }
    }
}
