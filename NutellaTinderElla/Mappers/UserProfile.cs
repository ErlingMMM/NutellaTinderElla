using AutoMapper;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderElla.Data.Dtos.ActiveUser;
using NutellaTinderElla.Data.Dtos.Matching;

namespace NutellaTinderEllaApi.Mappers
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<UserPostDTO, User>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<LikesPutDTO, User>().ReverseMap();
            CreateMap<SwipePutDTO, User>().ReverseMap();

        }
    }
}
