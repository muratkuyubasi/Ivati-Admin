using AutoMapper;
using ContentManagement.Data.Dto;
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
    public class UpdateAtaseCommandHandler : IRequestHandler<UpdateAtaseCommand, ServiceResponse<AtaseModelDTO>>
    {
        private readonly IAtaseRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateAtaseCommandHandler(IAtaseRepository ataseRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = ataseRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<AtaseModelDTO>> Handle(UpdateAtaseCommand request, CancellationToken cancellationToken)
        {
            var model = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<AtaseModelDTO>.Return409("Bu ID'ye ait bir ataşe bulunmamaktadır!");
            }
            model.Image = request.Image;
            model.Name = request.Name;
            model.Surname = request.Surname;
            model.JobDescription = request.JobDescription;
            model.PlaceOfDuty = request.PlaceOfDuty;
            repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<AtaseModelDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<AtaseModelDTO>.ReturnResultWith200(_mapper.Map<AtaseModelDTO>(model));
        }
    }
}
