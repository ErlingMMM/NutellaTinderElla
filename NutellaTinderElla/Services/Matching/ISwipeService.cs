

using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.Matching
{
    public interface ISwipeService : ICrudService<Swipes, int>
    {
        Task DeleteSwipeByIdAsync(int userId, int matchedUserId);

    }
}
