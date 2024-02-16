using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.Domain;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Commands;
using CenazeFonu.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CenazeFonu.MediatR.Handlers
{
    public class AddFamilyNoteCommandHandler : IRequestHandler<AddFamilyNoteCommand, ServiceResponse<FamilyNoteDTO>>
    {
        private readonly IFamilyNoteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddFamilyNoteCommandHandler(IFamilyNoteRepository repository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repository = repository; 
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<FamilyNoteDTO>> Handle(AddFamilyNoteCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<FamilyNote>(request);
            model.CreationDate = DateTime.Now;
            _repository.Add(model); 
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<FamilyNoteDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<FamilyNoteDTO>.ReturnResultWith200(_mapper.Map<FamilyNoteDTO>(model));
        }
    }
}
