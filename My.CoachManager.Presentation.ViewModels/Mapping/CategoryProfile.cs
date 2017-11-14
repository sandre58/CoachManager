using AutoMapper;
using My.CoachManager.Application.Dtos.Data;

namespace My.CoachManager.Presentation.ViewModels.Mapping
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDto, CategoryViewModel>().ReverseMap();
        }
    }
}