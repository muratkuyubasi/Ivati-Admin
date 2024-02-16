using AutoMapper;
using ContentManagement.Data;
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
    public class UpdateChairmanCommandHandler : IRequestHandler<UpdateChairmanCommand, ServiceResponse<ChairmanDTO>>
    {
        private readonly IChairmanRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateChairmanCommandHandler(IChairmanRepository chairmanRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = chairmanRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ChairmanDTO>> Handle(UpdateChairmanCommand request, CancellationToken cancellationToken)
        {
            var model = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<ChairmanDTO>.Return409("Bu ID'ye ait bir yönetim kurulu başkanı bulunmamaktadır!");
            }
            model.FullName = request.FullName;
            model.Title = request.Title;
            model.Description = request.Description;
            model.Image = request.Image;
            repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<ChairmanDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<ChairmanDTO>.ReturnResultWith200(_mapper.Map<ChairmanDTO>(model));
        }
    }
}
