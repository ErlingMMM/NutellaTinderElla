using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.Matching
{
    public interface ILikeService : ICrudService<Like, int>
    {
        Task<bool> HasMatchAsync(int likerId, int likedUser);
        Task DeleteLikeByIdAsync(int userId, int matchedUserId);
    }
}
