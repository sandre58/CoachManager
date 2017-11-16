using AutoMapper;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            // Persons
            CreateMap<Player, PlayerDto>().ReverseMap();
            CreateMap<Coach, CoachDto>().ReverseMap();
            CreateMap<Person, PersonDto>()
                .Include<Player, PlayerDto>()
                .Include<Coach, CoachDto>().ReverseMap();

            // Contacts
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Contact, ContactDto>()
                .Include<Email, EmailDto>()
                .Include<Phone, PhoneDto>().ReverseMap();

            // Foreign properties
            CreateMap<PlayerPosition, PlayerPositionDto>().ReverseMap();
            CreateMap<PlayerHeight, PlayerHeightDto>().ReverseMap();
            CreateMap<PlayerWeight, PlayerWeight>().ReverseMap();

            // Misc
            CreateMap<Country, CountryDto>().ReverseMap();
        }
    }
}