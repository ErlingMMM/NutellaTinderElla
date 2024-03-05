using AutoMapper;
using NutellaTinderEllaApi.Data.Dtos.Character;
using NutellaTinderEllaApi.Data.Dtos.Movie;
using NutellaTinderEllaApi.Data.Models;

namespace NutellaTinderEllaApi.Mappers
{
    public class CharacterProfile : Profile
    {
        //Mappers or AutoMapper profiles are used to map data between DTOs and models.
        public CharacterProfile() 
        {
            CreateMap<CharacterPostDTO, Character>().ReverseMap();
            CreateMap<Character, CharacterDTO>()
            .ForMember(
                dto => dto.Movies,
                opt => opt.MapFrom(src => src.Movies.Select(m => new MovieDTO { Id = m.Id })))
            .ReverseMap();
            CreateMap<CharacterPutDTO, Character>().ReverseMap();
            
            CreateMap<int, MovieDTO>().ConstructUsing(id => new MovieDTO { Id = id });
            CreateMap<MovieDTO, Movie>().ReverseMap();
        }
    }
}
