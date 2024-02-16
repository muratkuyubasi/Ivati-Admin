using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.DataDto;
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
