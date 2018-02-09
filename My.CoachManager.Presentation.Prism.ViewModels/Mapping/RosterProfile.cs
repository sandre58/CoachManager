using AutoMapper;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Application.Dtos.Rosters;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class RosterProfile : Profile
    {
        public RosterProfile()
        {
            CreateMap<RosterDto, RosterViewModel>().ReverseMap();
            CreateMap<SquadDto, SquadViewModel>().ReverseMap();
            CreateMap<RosterPlayerDto, RosterPlayerViewModel>().ReverseMap();
            CreateMap<RosterCoachDto, RosterCoachViewModel>().ReverseMap();
            CreateMap<SquadPlayerDto, SquadPlayerViewModel>().ReverseMap();
        }
    }
}