using ContentManagement.MediatR.Queries.User;
using ContentManagement.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ContentManagement.MediatR.Handlers
{
    public class GetUserCountByCityIdQueryHandler : IRequestHandler<GetUserCountByCityIdQuery, Object>
    {
        private readonly IUserRepository _userRepository;
        private readonly ICityRepository _cityRepository;
        public GetUserCountByCityIdQueryHandler(IUserRepository userRepository, ICityRepository cityRepository)
        {
            _userRepository = userRepository;
            _cityRepository = cityRepository;
        }
        public async Task<object> Handle(GetUserCountByCityIdQuery request, CancellationToken cancellationToken)
        {
            var city = _cityRepository.All.Where(X => X.Id == request.CityId).FirstOrDefault();
            var users =  _userRepository.All.Where(X => X.CityId == request.CityId)
                .GroupBy(x => x.CityId)
                .Select(x => new
            {
                City = _cityRepository.All.Where(x=>x.Id == request.CityId).FirstOrDefault().Name ?? null,
                TotalCount = x.Count(),
                ActiveUserCount = x.Count(y=>y.IsActive == true),
                PassiveUserCount = x.Count(Y=>Y.IsActive == false)
            }).OrderByDescending(x=>x.TotalCount).ToList();
            return users;
        }
    }
}
