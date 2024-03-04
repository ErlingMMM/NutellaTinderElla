using WebMovieApi.Data.Models;
using WebMovieApi.Data.Exceptions;
using Microsoft.EntityFrameworkCore;


namespace WebMovieApi.Services.Movies
{
    public interface IMovieService : ICrudService<Movie, int>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.

        //Updating characters in movie
        Task UpdateCharactersInMovieAsync(int movieId, int[] characterId);

        //Get all characters in movie
        Task<ICollection<Character>> GetAllCharactersInMovieAsync(int id);
    }
}
