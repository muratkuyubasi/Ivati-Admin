using AutoMapper;
using ContentManagement.Data;
using ContentManagement.Data.Dto;
using ContentManagement.Data.Models;
using ContentManagement.MediatR.Commands;

namespace ContentManagement.API.Helpers.Mapping
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
