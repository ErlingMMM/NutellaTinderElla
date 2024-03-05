using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data.Exceptions;


namespace NutellaTinderEllaApi.Services.Franchises
{
    public interface IFranchiseService : ICrudService<Franchise, int>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.

        //Get all movies in franchise
        Task<ICollection<Movie>> GetAllMoviesInFranchiseAsync(int id);

        //Update movies in franchise
        Task UpdateMoviesInFranchiseAsync(int franchiseId, int[] movieId);

        //Get all characters in franchise
        Task<ICollection<Character>> GetAllCharactersInFranchiseAsync(int id);


    }
}
