using AutoMapper;
using ContentManagement.Common.UnitOfWork;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.Domain;
using ContentManagement.Helper;
using ContentManagement.MediatR.Commands;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class AddActivityCommandHandler : IRequestHandler<AddActivityCommand, ServiceResponse<ActivityDTO>>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork<PTContext> _uow;
        private readonly IMapper _mapper;

        public AddActivityCommandHandler(IActivityRepository activityRepository, IUnitOfWork<PTContext> uow, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _uow = uow;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<ActivityDTO>> Handle(AddActivityCommand request, CancellationToken cancellationToken)
        {
            var data = _mapper.Map<Activity>(request);
            data.CreationDate = DateTime.Now;
            _activityRepository.Add(data);
            if (_uow.Save()<=0)
            {
                return ServiceResponse<ActivityDTO>.Return409("Kayıt sırasında bir hata meydana geldi!");
            }
            return ServiceResponse<ActivityDTO>.ReturnResultWith200(_mapper.Map<ActivityDTO>(data));
        }
    }
}
