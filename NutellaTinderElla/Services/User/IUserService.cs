using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.ActiveUser

{
    public interface IUserService : ICrudService<User, int>
    {
        Task<IEnumerable<int>> GetSwipedUserIdsAsync(int swipingUserId);
    }
}