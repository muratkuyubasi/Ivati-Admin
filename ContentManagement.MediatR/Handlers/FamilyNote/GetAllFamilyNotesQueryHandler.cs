using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.MediatR.Queries;
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
    public class GetAllFamilyNotesQueryHandler : IRequestHandler<GetAllFamilyNotesQuery, ServiceResponse<List<FamilyNoteDTO>>>
    {
        private readonly IFamilyNoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetAllFamilyNotesQueryHandler(IFamilyNoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }
        public async Task<ServiceResponse<List<FamilyNoteDTO>>> Handle(GetAllFamilyNotesQuery request, CancellationToken cancellationToken)
        {
            var notes = await _noteRepository.All.ToListAsync();
            return ServiceResponse<List<FamilyNoteDTO>>.ReturnResultWith200(_mapper.Map<List<FamilyNoteDTO>>(notes));
        }
    }
}
