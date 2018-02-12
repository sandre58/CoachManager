using System.Linq;
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
            CreateMap<PlayerDto, Player>().ReverseMap();
            CreateMap<CoachDto, Coach>().ReverseMap();
            CreateMap<PersonDto, Person>()
                .Include<PlayerDto, Player>()
                .Include<CoachDto, Coach>()
                .ForMember(x => x.Contacts, opt => opt.MapFrom(x => x.Emails.ToList().Cast<ContactDto>().Union(x.Phones.ToList())))
                .ForMember(x => x.Address, opt => opt.Ignore())
;

            // Contacts
            CreateMap<EmailDto, Email>().ReverseMap();
            CreateMap<PhoneDto, Phone>().ReverseMap();
            CreateMap<ContactDto, Contact>()
                .Include<EmailDto, Email>()
                .Include<PhoneDto, Phone>()
                .ReverseMap();

            // Foreign properties
            CreateMap<PlayerPositionDto, PlayerPosition>().ReverseMap();

            // Misc
            CreateMap<CountryDto, Country>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}