using System.Linq;
using AutoMapper;
using My.CoachManager.Application.Dtos.Administration;
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
                .ForMember(x => x.Emails, opt => opt.MapFrom(x => x.Contacts.OfType<Email>().ToList()))
                .ForMember(x => x.Phones, opt => opt.MapFrom(x => x.Contacts.OfType<Phone>().ToList()))
                .Include<Player, PlayerDto>()
                .Include<Coach, CoachDto>()
                .ReverseMap()
                .ForMember(x => x.Contacts, opt => opt.MapFrom(x => x.Emails.ToList().Cast<ContactDto>().Union(x.Phones.ToList())));

            // Contacts
            CreateMap<Email, EmailDto>().ReverseMap();
            CreateMap<Phone, PhoneDto>().ReverseMap();
            CreateMap<Contact, ContactDto>()
                .Include<Email, EmailDto>()
                .Include<Phone, PhoneDto>().ReverseMap();

            // Foreign properties
            CreateMap<PlayerPosition, PlayerPositionDto>().ReverseMap();

            // Misc
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}