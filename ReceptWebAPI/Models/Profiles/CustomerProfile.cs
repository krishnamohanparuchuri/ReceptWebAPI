using AutoMapper;
using ReceptWebAPI.Models.Domain;
using ReceptWebAPI.Models.DTO;

namespace ReceptWebAPI.Models.Profiles
{
    public class CustomerProfile : Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, CustomerResponseDto>()
                .ForMember(dest => dest.CustomerId, opt => opt.MapFrom(src => src.CustomerId))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email));

        }
    }
}
