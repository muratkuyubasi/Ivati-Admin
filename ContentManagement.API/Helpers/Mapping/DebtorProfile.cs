using AutoMapper;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
