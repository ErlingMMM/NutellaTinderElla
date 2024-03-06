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
            CreateMap<CurrentUserPostDTO, CurrentUser>().ReverseMap();
            CreateMap<CurrentUser, CurrentUserDTO>().ReverseMap();
            CreateMap<LikesPutDTO, CurrentUser>().ReverseMap();

        }
    }
}
