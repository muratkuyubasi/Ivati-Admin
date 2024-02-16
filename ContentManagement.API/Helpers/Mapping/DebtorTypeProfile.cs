using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
{
    public class DebtorTypeProfile : Profile
    {
        public DebtorTypeProfile()
        {
            CreateMap<DebtorType, DebtorTypeDTO>().ReverseMap();
            CreateMap<AddDebtorTypeCommand, DebtorType>();
            CreateMap<UpdateDebtorTypeCommand, DebtorType>();
        }
    }
}
