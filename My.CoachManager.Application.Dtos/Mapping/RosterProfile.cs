using AutoMapper;
using My.CoachManager.Application.Dtos.Rosters;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public class RosterProfile : Profile
    {
        public RosterProfile()
        {
            CreateMap<Roster, RosterDto>().ReverseMap();
            CreateMap<Squad, SquadDto>().ReverseMap();
            CreateMap<RosterPlayer, RosterPlayerDto>().ReverseMap();
            CreateMap<RosterCoach, RosterCoachDto>().ReverseMap();
        }
    }
}