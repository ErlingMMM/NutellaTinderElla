using Microsoft.Extensions.FileSystemGlobbing;
using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.Matching
{
    public interface IMatchService : ICrudService<Match, int>
    {
        Task DeleteMatchByIdAsync(int userId, int matchedUserId);
        Task<bool> HasMatchAsync(int matcherId, int matchingUserId);
    }
}
