using AutoMapper;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderElla.Data.Dtos.ActiveUser;


namespace NutellaTinderEllaApi.Mappers
{
    public class UserProfile : Profile
    {
        //Mappers or AutoMapper profiles are used to map data between DTOs and models.
        public UserProfile()
        {
            CreateMap<CurrentUserPostDTO, CurrentUser>().ReverseMap();
            CreateMap<CurrentUser, CurrentUserDTO>().ReverseMap();
        }
    }
}
