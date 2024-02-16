using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Helper;
using CenazeFonu.MediatR.Queries;
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
    public class GetUsersNoteByUserIdQueryHandler : IRequestHandler<GetUsersNoteByUserIdQuery, ServiceResponse<List<NoteDTO>>>
    {
        private readonly INoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetUsersNoteByUserIdQueryHandler(INoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<NoteDTO>>> Handle(GetUsersNoteByUserIdQuery request, CancellationToken cancellationToken)
        {
            var userNotes = await _noteRepository.All.Where(x => x.UserId == request.UserId).ToListAsync();
            return ServiceResponse<List<NoteDTO>>.ReturnResultWith200(_mapper.Map<List<NoteDTO>>(userNotes));
        }
    }
}
