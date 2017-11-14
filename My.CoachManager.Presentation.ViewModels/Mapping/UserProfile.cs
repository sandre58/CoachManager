using AutoMapper;
using My.CoachManager.Application.Dtos.Users;

namespace My.CoachManager.Presentation.ViewModels.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserDto, UserViewModel>().ReverseMap();
            CreateMap<RoleDto, RoleViewModel>().ReverseMap();
            CreateMap<PermissionDto, PermissionViewModel>().ReverseMap();
        }
    }
}