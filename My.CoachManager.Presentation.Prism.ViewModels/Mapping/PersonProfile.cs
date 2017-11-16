using System.Linq;
using AutoMapper;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {

            // Persons
            CreateMap<PlayerDto, PlayerViewModel>().ReverseMap();
            CreateMap<CoachDto, CoachViewModel>().ReverseMap();
            CreateMap<PersonDto, PersonViewModel>()
                .ForMember(x => x.Emails, opt => opt.MapFrom(x => x.Contacts.OfType<EmailDto>()))
                .ForMember(x => x.Phones, opt => opt.MapFrom(x => x.Contacts.OfType<PhoneDto>()))
                .Include<PlayerDto, PlayerViewModel>()
                .Include<CoachDto, CoachViewModel>().ReverseMap()
                .ForMember(x => x.Contacts, opt => opt.MapFrom(x => x.Emails.Cast<ContactViewModel>().Union(x.Phones)));

            // Contacts
            CreateMap<EmailDto, EmailViewModel>().ReverseMap();
            CreateMap<PhoneDto, PhoneViewModel>().ReverseMap();
            CreateMap<ContactDto, ContactViewModel>()
                .Include<EmailDto, EmailViewModel>()
                .Include<PhoneDto, PhoneViewModel>().ReverseMap();

            // Foreign properties
            CreateMap<PlayerPositionDto, PlayerPositionViewModel>().ReverseMap();
            CreateMap<PlayerHeightDto, PlayerHeightViewModel>().ReverseMap();
            CreateMap<PlayerWeightDto, PlayerWeightViewModel>().ReverseMap();

            // Misc
            CreateMap<CountryDto, CountryViewModel>().ReverseMap();
            CreateMap<CityDto, CityViewModel>().ReverseMap();
        }
    }
}