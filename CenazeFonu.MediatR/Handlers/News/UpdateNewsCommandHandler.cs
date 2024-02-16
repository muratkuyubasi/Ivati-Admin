using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.DataDto;
using CenazeFonu.Domain;
using CenazeFonu.Domain.Migrations;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Commands
{
    public class UpdateNewsCommandHandler : IRequestHandler<UpdateNewsCommand, ServiceResponse<NewsDTO>>
    {
        private readonly INewsRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateNewsCommandHandler(INewsRepository news, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = news;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<NewsDTO>> Handle(UpdateNewsCommand request, CancellationToken cancellationToken)
        {
            var model = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<NewsDTO>.Return409("Bu ID'ye ait bir haber bulunmamaktadır!");
            }
            model.Title = request.Title;
            model.Description = request.Description;
            model.Image = request.Image;
            model.IsActive = request.IsActive;
            model.CreatedBy = request.CreatedBy;
            model.UpdatedBy = request.UpdatedBy;
            model.CreationDate = request.CreationDate;
            model.UpdatedDate = request.UpdatedDate;
            repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<NewsDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<NewsDTO>.ReturnResultWith200(_mapper.Map<NewsDTO>(model));
        }
    }
}
