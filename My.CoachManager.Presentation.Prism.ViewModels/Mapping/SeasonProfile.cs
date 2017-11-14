using AutoMapper;
using My.CoachManager.Application.Dtos.Admin;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class SeasonProfile : Profile
    {
        public SeasonProfile()
        {
            CreateMap<SeasonDto, SeasonViewModel>().ReverseMap();
        }
    }
}