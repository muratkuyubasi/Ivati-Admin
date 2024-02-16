using AutoMapper;
using CenazeFonu.Data;
using CenazeFonu.Data.Dto;
using CenazeFonu.Data.Models;
using CenazeFonu.MediatR.Commands;

namespace CenazeFonu.API.Helpers.Mapping
{
    public class AddressProfile : Profile
    {

        public AddressProfile()
        {
            CreateMap<Address, AddressDTO>().ReverseMap();
            CreateMap<AddUserModelCommand, UserModel>().ReverseMap();
            CreateMap<UpdateFamilyAddressCommand, Address>();
        }
    }
}
