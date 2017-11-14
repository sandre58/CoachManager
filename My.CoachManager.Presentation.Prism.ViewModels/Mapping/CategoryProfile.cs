using AutoMapper;
using My.CoachManager.Application.Dtos.Admin;

namespace My.CoachManager.Presentation.Prism.ViewModels.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
        }
    }
}