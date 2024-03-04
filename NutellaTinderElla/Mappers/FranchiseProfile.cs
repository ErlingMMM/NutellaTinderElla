using Microsoft.Extensions.Options;
using AutoMapper;
using WebMovieApi.Data.Models;
using WebMovieApi.Data.Dtos.Franchise;
using WebMovieApi.Data.Dtos.Movie;

namespace WebMovieApi.Mappers
{
    public class FranchiseProfile : Profile
    {
        //Mappers or AutoMapper profiles are used to map data between DTOs and models.
        public FranchiseProfile()
        {
            CreateMap<Franchise, FranchisePostDTO>().ReverseMap(); 
            CreateMap<Franchise, FranchiseDTO>().ForMember(fdto => fdto.Movies, options => options.MapFrom(f => f.Movies.Select(m => m.Id).ToArray()));
            CreateMap<Franchise, FranchisePutDTO>().ReverseMap();
        }
    }
}
