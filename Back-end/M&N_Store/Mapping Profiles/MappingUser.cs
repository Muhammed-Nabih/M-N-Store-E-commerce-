using AutoMapper;
using M_N_Store.Domain.Dtos;
using M_N_Store.Domain.Entities;

namespace M_N_Store.Mapping_Profiles
{
    public class MappingUser : Profile
    {
        public MappingUser()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
