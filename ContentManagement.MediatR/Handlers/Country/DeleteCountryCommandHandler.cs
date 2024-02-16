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
    public class DeleteCountryCommandHandler : IRequestHandler<DeleteCountryCommand, ServiceResponse<CountryDTO>>
    {
        private readonly ICountryRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteCountryCommandHandler(ICountryRepository countryRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = countryRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<CountryDTO>> Handle(DeleteCountryCommand request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<CountryDTO>.Return409("Bu ID'ye ait bir ülke bulunamadı!");
            }
            _repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<CountryDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<CountryDTO>.ReturnResultWith200(_mapper.Map<CountryDTO>(data));
        }
    }
}
