using AutoMapper;
using CenazeFonu.Common.UnitOfWork;
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
    public class AddNoteCommandHandler : IRequestHandler<AddNoteCommand, ServiceResponse<NoteDTO>>
    {
        private readonly INoteRepository _repository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork<PTContext> _uow;

        public AddNoteCommandHandler(INoteRepository repository, IMapper mapper, IUnitOfWork<PTContext> uow)
        {
            _repository = repository;
            _mapper = mapper;
            _uow = uow;
        }

        public async Task<ServiceResponse<NoteDTO>> Handle(AddNoteCommand request, CancellationToken cancellationToken)
        {
            var model = _mapper.Map<Note>(request);
            model.CreationDate = DateTime.Now;
            _repository.Add(model);
            if (await _uow.SaveAsync()<=0)
            {
                return ServiceResponse<NoteDTO>.Return409("Ekleme işlemi sırasında bir hata meydana geldi!");
            }
            else return ServiceResponse<NoteDTO>.ReturnResultWith200(_mapper.Map<NoteDTO>(model));
        }
    }
}
