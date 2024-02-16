using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data.Dto;
using CenazeFonu.Domain;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CenazeFonu.Helper;

namespace CenazeFonu.MediatR.Handlers
{
    public class DeletePageCommandHandler : IRequestHandler<DeletePageCommand, ServiceResponse<PageDto>>
    {
        private readonly IPageRepository _pageRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        public DeletePageCommandHandler(
           IPageRepository pageRepository,
            IUnitOfWork<PTContext> uow
            )
        {
            _pageRepository = pageRepository;
            _uow = uow;
        }

        public async Task<ServiceResponse<PageDto>> Handle(DeletePageCommand request, CancellationToken cancellationToken)
        {
            var entityExist = await _pageRepository.FindAsync(request.Id);
            if (entityExist == null)
            {
                return ServiceResponse<PageDto>.Return404();
            }
            _pageRepository.Delete(request.Id);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<PageDto>.Return500();
            }
            return ServiceResponse<PageDto>.ReturnSuccess();
        }
    }
}
