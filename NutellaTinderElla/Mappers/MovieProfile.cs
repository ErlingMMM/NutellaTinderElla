using AutoMapper;
using WebMovieApi.Data.Dtos.Movie;
using WebMovieApi.Data.Models;

namespace WebMovieApi.Mappers
{
    public class MovieProfile : Profile 
    {
        //Mappers or AutoMapper profiles are used to map data between DTOs and models.
        public MovieProfile() 
        {
            CreateMap<Movie, MoviePutDTO>().ReverseMap();
            CreateMap<Movie, MovieDTO>()
                .ForMember(mdto => mdto.FranchiseId, options => options.MapFrom(m => m.FranchiseId))
                .ForMember(mdto => mdto.Characters, options => options.MapFrom(m => m.Characters.Select(c => c.Id).ToArray()));
            CreateMap<Movie, MoviePostDTO>().ReverseMap();
        }
    }
}
