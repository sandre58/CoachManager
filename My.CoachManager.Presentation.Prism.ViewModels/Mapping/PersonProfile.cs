using AutoMapper;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class PersonProfile : Profile
    {
        public PersonProfile()
        {
            CreateMap<EmailDto, EmailViewModel>().ReverseMap();
            CreateMap<PhoneDto, PhoneViewModel>().ReverseMap();
            CreateMap<CountryDto, CountryViewModel>().ReverseMap();
            CreateMap<CityDto, CityViewModel>().ReverseMap();
            CreateMap<PlayerPositionDto, PlayerPositionViewModel>().ReverseMap();
            CreateMap<PlayerHeightDto, PlayerHeightViewModel>().ReverseMap();
            CreateMap<PlayerWeightDto, PlayerWeightViewModel>().ReverseMap();
            CreateMap<PlayerDto, PlayerViewModel>().ReverseMap();
            CreateMap<CoachDto, CoachViewModel>().ReverseMap();
            CreateMap<ContactDto, ContactViewModel>().ReverseMap();
        }
    }
}