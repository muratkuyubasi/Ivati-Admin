using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeletePageActionCommandHandler : IRequestHandler<DeletePageActionCommand, ServiceResponse<PageActionDto>>
    {
        private readonly IPageActionRepository _pageActionRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;
        public DeletePageActionCommandHandler(
           IPageActionRepository pageActionRepository,
            IMapper mapper,
            IUnitOfWork<PTContext> uow
            )
        {
            _pageActionRepository = pageActionRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageActionDto>> Handle(DeletePageActionCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageActionRepository.FindBy(c => c.Id == request.Id).FirstOrDefaultAsync();
            if (entityExist == null)
            {
                return ServiceResponse<PageActionDto>.Return404();
            }
            _pageActionRepository.Remove(entityExist);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageActionDto>.Return500();
            }
            return ServiceResponse<PageActionDto>.ReturnSuccess();
        }
    }
}
