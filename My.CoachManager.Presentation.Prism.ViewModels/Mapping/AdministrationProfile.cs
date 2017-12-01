using AutoMapper;
using My.CoachManager.Application.Dtos.Administration;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class AdministrationProfile : Profile
    {
        public AdministrationProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
            CreateMap<PositionDto, PositionViewModel>().ReverseMap();
            CreateMap<SeasonDto, SeasonViewModel>().ReverseMap();
            CreateMap<AddressDto, AddressViewModel>().ReverseMap();
            CreateMap<FunctionDto, FunctionViewModel>().ReverseMap();
        }
    }
}