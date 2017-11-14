using AutoMapper;
using My.CoachManager.Application.Dtos.Players;

namespace My.CoachManager.Presentation.ViewModels.Mapping
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerDto, PlayerViewModel>().ReverseMap();
            CreateMap<ContactDto, PhoneViewModel>().ReverseMap();
            CreateMap<ContactDto, EmailViewModel>().ReverseMap();
        }
    }
}