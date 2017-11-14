using AutoMapper;
using My.CoachManager.Application.Dtos.Users;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Mapping
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Role, RoleDto>().ReverseMap();
            CreateMap<Permission, PermissionDto>().ReverseMap();
        }
    }
}