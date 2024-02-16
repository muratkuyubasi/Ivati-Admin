using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Helper;
using ContentManagement.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Queries
{
    public class GetAssociationByIdQueryHandler : IRequestHandler<GetAssociationByIdQuery,ServiceResponse<AssociationDTO>>
    {
        private readonly IAssociationRepository _associationRepository;
        private readonly IMapper _mapper;

        public GetAssociationByIdQueryHandler(IAssociationRepository associationRepository, IMapper mapper)
        {
            _associationRepository = associationRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<AssociationDTO>> Handle(GetAssociationByIdQuery request, CancellationToken cancellationToken)
        {
            var data = await _associationRepository.FindBy(x=>x.Id == request.Id).FirstOrDefaultAsync();
            if (data == null)
            {
                return ServiceResponse<AssociationDTO>.Return409("Bu ID'ye ait bir dernek bulunamadı!");
            }
            else return ServiceResponse<AssociationDTO>.ReturnResultWith200(_mapper.Map<AssociationDTO>(data));
        }
    }
}
