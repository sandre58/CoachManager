using AutoMapper;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public class AdminProfile : Profile
    {
        public AdminProfile()
        {
            // Categories
            CreateMap<Category, CategoryDto>().ReverseMap();

            // Positions
            CreateMap<Position, PositionDto>().ReverseMap();

            // Seasons
            CreateMap<Season, SeasonDto>().ReverseMap();

            // Functions
            CreateMap<Function, FunctionDto>().ReverseMap();
        }
    }
}