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
    public class UpdateFamilyNoteCommandHandler : IRequestHandler<UpdateFamilyNoteCommand, ServiceResponse<FamilyNoteDTO>>
    {
        private readonly IFamilyNoteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public UpdateFamilyNoteCommandHandler(IFamilyNoteRepository repository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FamilyNoteDTO>> Handle(UpdateFamilyNoteCommand request, CancellationToken cancellationToken)
        {
            var data =  _repository.FindBy(x => x.Id == request.Id).FirstOrDefault();
            if (data == null)
            {
                return ServiceResponse<FamilyNoteDTO>.Return409("Bu ID'ye ait bir not bulunmamaktadır!");
            }
            data.Text = request.Text;
            data.UpdateDate = DateTime.Now;
            data.isDeleted = request.isDeleted;
            _repository.Update(data);
            if (await _uow.SaveAsync() <= 0)
            {
                return ServiceResponse<FamilyNoteDTO>.Return409("Güncelleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FamilyNoteDTO>.ReturnResultWith200(_mapper.Map<FamilyNoteDTO>(data));
        }
    }
}
