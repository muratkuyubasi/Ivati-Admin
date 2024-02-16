using AutoMapper;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class DebtorProfile : Profile
    {

        public DebtorProfile()
        {
            CreateMap<Debtor, DebtorDTO>().ReverseMap();
            CreateMap<Debtor, DebtorFamilyDTO>().ReverseMap();
            CreateMap<Debtor, DebtorByYearDTO>().ReverseMap();
            CreateMap<UpdateDebtorCommand, Debtor>();
            CreateMap<UpdateDebtorsCommand, Debtor>();
            CreateMap<AddDebtorCommand, Debtor>();
            CreateMap<Debtor, DebtorSimpleDTO>().ReverseMap();
            CreateMap<PrintAllDebtorsToFileCommand, Debtor>();
        }
    }
}
