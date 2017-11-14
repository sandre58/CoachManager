using AutoMapper;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<PlayerPosition, PlayerPositionDto>().ReverseMap();
            CreateMap<PlayerHeight, PlayerHeightDto>().ReverseMap();
            CreateMap<PlayerWeight, PlayerWeight>().ReverseMap();
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Coach, CoachDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}