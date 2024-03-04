using AutoMapper;
using WebMovieApi.Data.Dtos.Character;
using WebMovieApi.Data.Dtos.Movie;
using WebMovieApi.Data.Models;

namespace WebMovieApi.Mappers
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
