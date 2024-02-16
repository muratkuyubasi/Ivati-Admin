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
    public class GetFamilyNoteByIdQueryHandler : IRequestHandler<GetFamilyNoteByIdQuery, ServiceResponse<FamilyNoteDTO>>
    {
        private readonly IFamilyNoteRepository _noteRepository;
        private readonly IMapper _mapper;

        public GetFamilyNoteByIdQueryHandler(IFamilyNoteRepository noteRepository, IMapper mapper)
        {
            _noteRepository = noteRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<FamilyNoteDTO>> Handle(GetFamilyNoteByIdQuery request, CancellationToken cancellationToken)
        {
            var note = await _noteRepository.FindBy(x => x.Id == request.Id).FirstOrDefaultAsync();
            return ServiceResponse<FamilyNoteDTO>.ReturnResultWith200(_mapper.Map<FamilyNoteDTO>(note));
        }
    }
}
