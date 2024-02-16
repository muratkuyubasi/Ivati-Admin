using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.DataDto;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Commands
{
    public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, ServiceResponse<NewsDTO>>
    {
        private readonly INewsRepository repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddNewsCommandHandler(INewsRepository newsRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            repo = newsRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<NewsDTO>> Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<News>(request);
            repo.Add(model);           
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<NewsDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else
                return ServiceResponse<NewsDTO>.ReturnResultWith200(_mapper.Map<NewsDTO>(model));
        }
    }
}
