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
    public class DeleteHacFormCommandHandler : IRequestHandler<DeleteHacFormCommand, ServiceResponse<HacFormDTO>>
    {
        private readonly IHacRepository _hacRepository;
        private readonly IMapper _mapper;
        private IUnitOfWork<PTContext> _uow;

        public DeleteHacFormCommandHandler(IHacRepository hacRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _hacRepository = hacRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<HacFormDTO>> Handle(DeleteHacFormCommand request, CancellationToken cancellationToken)
        {
            var data = await _hacRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<HacFormDTO>.Return409("Bu ID'ye ait bir başvuru bulunamadı!");
            }
            _hacRepository.Remove(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<HacFormDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<HacFormDTO>.ReturnResultWith200(_mapper.Map<HacFormDTO>(data));
        }
    }
}
