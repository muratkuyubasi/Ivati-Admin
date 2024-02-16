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
    public class DeleteUmreFormCommandHandler : IRequestHandler<DeleteUmreFormCommand, ServiceResponse<UmreFormDTO>>
    {
        private readonly IUmreFormRepository _repo;
        private readonly IMapper _mapper;
        private IUnitOfWork<PTContext> _uow;

        public DeleteUmreFormCommandHandler(IUmreFormRepository umreFormRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = umreFormRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<UmreFormDTO>> Handle(DeleteUmreFormCommand request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<UmreFormDTO>.Return409("Bu ID'ye ait bir başvuru bulunamadı!");
            }
            _repo.Remove(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<UmreFormDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<UmreFormDTO>.ReturnResultWith200(_mapper.Map<UmreFormDTO>(data));
        }
    }
}
