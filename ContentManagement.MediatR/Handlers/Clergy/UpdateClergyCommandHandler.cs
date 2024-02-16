using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Domain.Migrations;
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

namespace ContentManagement.MediatR.Commands
{
    public class UpdateClergyCommandHandler : IRequestHandler<UpdateClergyCommand, ServiceResponse<ClergyDTO>>
    {
        private readonly IClergyRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateClergyCommandHandler(IClergyRepository clergyRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = clergyRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ClergyDTO>> Handle(UpdateClergyCommand request, CancellationToken cancellationToken)
        {
            var model = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<ClergyDTO>.Return409("Bu ID'ye ait bir din görevlisi bulunmamaktadır!");
            }
            model.Image = request.Image;
            model.Name = request.Name;
            model.Surname = request.Surname;
            model.JobDescription = request.JobDescription;
            model.PlaceOfDuty = request.PlaceOfDuty;
            repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ClergyDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<ClergyDTO>.ReturnResultWith200(_mapper.Map<ClergyDTO>(model));
        }
    }
}
