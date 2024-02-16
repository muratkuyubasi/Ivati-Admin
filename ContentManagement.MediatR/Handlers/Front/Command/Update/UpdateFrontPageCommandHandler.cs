using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Domain;
using PT.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;
using ContentManagement.Helper;
using Microsoft.Extensions.Logging;

namespace ContentManagement.MediatR.Handlers
{
    public class UpdateFrontPageCommandHandler : IRequestHandler<UpdateFrontPageCommand, ServiceResponse<FrontPageDto>>
    {
        private readonly IFrontPageRepository _FrontPageRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateFrontPageCommandHandler> _logger;
        public UpdateFrontPageCommandHandler(
           IFrontPageRepository FrontPageRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow,
            ILogger<UpdateFrontPageCommandHandler> logger
            )
        {
            _FrontPageRepository = FrontPageRepository;
            _mapper = mapper;
            _uow = uow;
            _logger = logger;
        }
        public async Task<ServiceResponse<FrontPageDto>> Handle(UpdateFrontPageCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Data.FrontPage>(request);
            _FrontPageRepository.Update(entity);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FrontPageDto>.Return500();
            }
            var entityDto = _mapper.Map<FrontPageDto>(entity);
            return ServiceResponse<FrontPageDto>.ReturnResultWith200(entityDto);
        }
    }
}
