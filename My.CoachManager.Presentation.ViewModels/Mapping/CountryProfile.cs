using AutoMapper;
using My.CoachManager.Application.Dtos.Data;

namespace My.CoachManager.Presentation.ViewModels.Mapping
{
    public class CountryProfile : Profile
    {
        public CountryProfile()
        {
            CreateMap<CountryDto, CountryViewModel>().ReverseMap();
        }
    }
}