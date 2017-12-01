using AutoMapper;
using My.CoachManager.Application.Dtos.Competitions;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public class CompetitionProfile : Profile
    {
        public CompetitionProfile()
        {
            CreateMap<Club, ClubDto>().ReverseMap();
            CreateMap<Team, TeamDto>().ReverseMap();
        }
    }
}