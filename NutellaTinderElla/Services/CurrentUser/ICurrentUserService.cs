using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.ActiveUser

{
    public interface ICurrentUserService : ICrudService<CurrentUser, int>
    {
        //Define the methods and operations that services must implement.
        //Provide a level of abstraction and help in unit testing and mocking.
    }
}