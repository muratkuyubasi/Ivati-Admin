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
    public class UpdateCityCommandHandler : IRequestHandler<UpdateCityCommand, ServiceResponse<CityDTO>>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = cityRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CityDTO>> Handle(UpdateCityCommand request, CancellationToken cancellationToken)
        {
            var model = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (model == null)
            {
                return ServiceResponse<CityDTO>.Return409("Bu ID'ye ait bir havaalanı bulunmamaktadır!");
            }
            model.CountryId = request.CountryId;
            model.Name = request.Name;
            model.Longitude = request.Longitude;
            model.Latitude = request.Latitude;
            _repo.Update(model);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<CityDTO>.Return409("Güncelleme işlemi sırasında bir hata oluştu");
            }
            else
                return ServiceResponse<CityDTO>.ReturnResultWith200(_mapper.Map<CityDTO>(model));
        }
    }
}
