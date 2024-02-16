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
    public class UpdateMosqueCommandHandler : IRequestHandler<UpdateMosqueCommand, ServiceResponse<MosqueDTO>>
    {
        private readonly IMosqueRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateMosqueCommandHandler(IMosqueRepository mosqueRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = mosqueRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<MosqueDTO>> Handle(UpdateMosqueCommand request, CancellationToken cancellationToken)
        {
            var model = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<MosqueDTO>.Return409("Bu ID'ye ait bir cami bulunmamaktadır!");
            }
            model.Image = request.Image;
            model.Name = request.Name;
            model.State = request.State;
            model.Address = request.Address;
            model.PhoneNumber = request.PhoneNumber;
            model.Ownership = request.Ownership;
            model.OfficersCount = request.OfficersCount;
            model.ImamFullName = request.ImamFullName;
            model.Group = request.Group;
            model.LodgingCount = request.LodgingCount;
            model.Capacity = request.Capacity;
            model.ExplanationAboutMosque = request.ExplanationAboutMosque;
            model.OpeningDate = request.OpeningDate;
            repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<MosqueDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<MosqueDTO>.ReturnResultWith200(_mapper.Map<MosqueDTO>(model));
        }
    }
}
