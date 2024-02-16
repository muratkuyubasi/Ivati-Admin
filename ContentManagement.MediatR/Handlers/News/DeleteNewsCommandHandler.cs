using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.DataDto;
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
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, ServiceResponse<NewsDTO>>
    {
        private readonly INewsRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteNewsCommandHandler(INewsRepository news, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = news;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<NewsDTO>> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var data = await repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<NewsDTO>.Return409("Bu ID'ye ait bir haber bulunamadı!");
            }
            repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<NewsDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<NewsDTO>.ReturnResultWith200(_mapper.Map<NewsDTO>(data));
        }
    }
}
