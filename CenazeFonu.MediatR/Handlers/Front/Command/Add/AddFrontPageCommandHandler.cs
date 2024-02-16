using AutoMapper;
using PT.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.Repository;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Domain;

namespace CenazeFonu.MediatR.Handlers
{
    public class AddFrontPageCommandHandler : IRequestHandler<AddFrontPageCommand, ServiceResponse<FrontPageDto>>
    {
        private readonly IFrontPageRepository _FrontPageRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<AddFrontPageCommandHandler> _logger;
        public AddFrontPageCommandHandler(
           IFrontPageRepository FrontPageRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<AddFrontPageCommandHandler> logger
            )
        {
            _FrontPageRepository = FrontPageRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontPageDto>> Handle(AddFrontPageCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontPage>(request);
            entity.Code = Guid.NewGuid();
            foreach(var item in entity.FrontPageRecords)
            {
                item.Code = Guid.NewGuid();
            }
            _FrontPageRepository.Add(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontPageDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontPageDto>(entity);
            return ServiceResponse<FrontPageDto>.ReturnResultWith200(entityDto);
        }
    }
}
