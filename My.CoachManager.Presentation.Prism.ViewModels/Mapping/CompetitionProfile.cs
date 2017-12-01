using AutoMapper;
using My.CoachManager.Application.Dtos.Competitions;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class CompetitionProfile : Profile
    {
        public CompetitionProfile()
        {
            // Persons
            CreateMap<ClubDto, ClubViewModel>().ReverseMap();
            CreateMap<TeamDto, TeamViewModel>().ReverseMap();
        }
    }
}