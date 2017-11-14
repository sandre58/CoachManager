using AutoMapper;
using My.CoachManager.Application.Dtos.Admin;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class PositionProfile : Profile
    {
        public PositionProfile()
        {
            CreateMap<PositionDto, PositionViewModel>().ReverseMap();
        }
    }
}