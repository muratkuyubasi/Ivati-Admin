using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
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
