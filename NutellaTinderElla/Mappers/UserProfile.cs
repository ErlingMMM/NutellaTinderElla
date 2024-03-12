using AutoMapper;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderElla.Data.Dtos.UserData;
using NutellaTinderElla.Data.Dtos.Matching;
using NutellaTinderElla.Data.Dtos.Messaging;

namespace NutellaTinderEllaApi.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserPostDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserPublicDataDTO>().ReverseMap();
            CreateMap<LikesPostDTO, User>().ReverseMap();
            CreateMap<SwipePostDTO, User>().ReverseMap();
            CreateMap<User, MessagePostDTO>().ReverseMap();
        }
    }
}
