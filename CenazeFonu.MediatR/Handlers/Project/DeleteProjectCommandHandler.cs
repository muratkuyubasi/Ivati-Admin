using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Common.UnitOfWork;
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
    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, ServiceResponse<ProjectDTO>>
    {
        private readonly IProjectRepository _repo;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public DeleteProjectCommandHandler(IProjectRepository translationRepository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repo = translationRepository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<ProjectDTO>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var data = await _repo.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<ProjectDTO>.Return409("Bu ID'ye ait bir proje bulunamadı!");
            }
            _repo.Remove(data);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<ProjectDTO>.Return409("Silme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<ProjectDTO>.ReturnResultWith200(_mapper.Map<ProjectDTO>(data));
        }
    }
}
