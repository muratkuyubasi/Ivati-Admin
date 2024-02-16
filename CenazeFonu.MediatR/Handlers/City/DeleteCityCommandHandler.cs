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
    public class DeleteCityCommandHandler : IRequestHandler<DeleteCityCommand, ServiceResponse<CityDTO>>
    {
        private readonly ICityRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteCityCommandHandler(ICityRepository cityRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = cityRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CityDTO>> Handle(DeleteCityCommand request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<CityDTO>.Return409("Bu ID'ye ait bir şehir bulunamadı!");
            }
            _repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<CityDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<CityDTO>.ReturnResultWith200(_mapper.Map<CityDTO>(data));
        }
    }
}
