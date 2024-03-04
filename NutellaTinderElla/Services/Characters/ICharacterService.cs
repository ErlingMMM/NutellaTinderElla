using WebMovieApi.Data.Models;
using WebMovieApi.Data.Exceptions;


namespace WebMovieApi.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.
    }
}
