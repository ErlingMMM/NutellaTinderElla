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
            /*  CreateMap<CurrentUser, CurrentUserDTO>()
              .ForMember(
                  dto => dto.Movies,
                  opt => opt.MapFrom(src => src.Movies.Select(m => new MovieDTO { Id = m.Id })))
              .ReverseMap();*/
            //CreateMap<CharacterPutDTO, Character>().ReverseMap();

            // CreateMap<int, MovieDTO>().ConstructUsing(id => new MovieDTO { Id = id });
            //  CreateMap<MovieDTO, Movie>().ReverseMap();
        }
    }
}
