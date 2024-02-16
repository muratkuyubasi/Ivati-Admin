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
    public class GetFamilyNoteByFamilyIdQueryHandler : IRequestHandler<GetFamilyNoteByFamilyIdQuery, ServiceResponse<List<FamilyNoteDTO>>>
    {
        private readonly IFamilyNoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetFamilyNoteByFamilyIdQueryHandler(IFamilyNoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<FamilyNoteDTO>>> Handle(GetFamilyNoteByFamilyIdQuery request, CancellationToken cancellationToken)
        {
            var userNotes = await _noteRepository.All.Where(x => x.FamilyId == request.FamilyId).ToListAsync();
            return ServiceResponse<List<FamilyNoteDTO>>.ReturnResultWith200(_mapper.Map<List<FamilyNoteDTO>>(userNotes));
        }
    }
}
