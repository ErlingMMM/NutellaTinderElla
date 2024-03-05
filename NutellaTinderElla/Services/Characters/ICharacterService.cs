using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data.Exceptions;


namespace NutellaTinderEllaApi.Services.Characters
{
    public interface ICharacterService : ICrudService<Character, int>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.
    }
}
