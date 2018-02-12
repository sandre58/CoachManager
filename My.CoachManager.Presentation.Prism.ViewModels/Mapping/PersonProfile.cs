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
            CreateMap<PlayerDetailsDto, PlayerDetailViewModel>().ReverseMap();

            // Contacts
            CreateMap<EmailDto, EmailViewModel>().ReverseMap();
            CreateMap<PhoneDto, PhoneViewModel>().ReverseMap();
            CreateMap<ContactDto, ContactViewModel>()
                .Include<EmailDto, EmailViewModel>()
                .Include<PhoneDto, PhoneViewModel>().ReverseMap();

            // Foreign properties
            CreateMap<PlayerPositionDto, PlayerPositionViewModel>().ReverseMap();

            // Misc
            CreateMap<CountryDto, CountryViewModel>().ReverseMap();
            CreateMap<CityDto, CityViewModel>().ReverseMap();
            CreateMap<AddressDto, AddressViewModel>().ReverseMap();
        }
    }
}