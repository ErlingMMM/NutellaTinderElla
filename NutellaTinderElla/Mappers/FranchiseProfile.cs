using Microsoft.Extensions.Options;
using AutoMapper;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data.Dtos.Franchise;
using NutellaTinderEllaApi.Data.Dtos.Movie;

namespace NutellaTinderEllaApi.Mappers
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
