using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data;
using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Data.Models;

namespace NutellaTinderElla.Services.Matching
{
    public class LikeService : ILikeService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly TinderDbContext _context;

        public LikeService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Like>> GetAllAsync()
        {
            return await _context.Likes.ToListAsync();
        }

        public async Task<Like> GetByIdAsync(int id)
        {
            var curUs = await _context.Likes.Where(c => c.Id == id).FirstAsync();

            if (curUs is null)
                throw new EntityNotFoundException(nameof(curUs), id);

            return curUs;
        }
        public async Task<Like> AddAsync(Like obj)
        {
            await _context.Likes.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id)
        {
            var likes = await _context.Likes.Where(l =>
                       (l.LikerId == id || l.LikedUserId == id)).ToListAsync();

            if (likes.Any())
            {
                _context.Likes.RemoveRange(likes);
                await _context.SaveChangesAsync();
            }

        }




        public async Task<Like> UpdateAsync(Like obj)
        {
            if (!await UserExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Like), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }


        public async Task<bool> HasMatchAsync(int likerId, int likedUser)
        {
            var hasMatch = await _context.Likes
                .AnyAsync(l => l.LikedUserId == likerId && l.LikerId == likedUser);

            return hasMatch;
        }


        public async Task DeleteLikeByIdAsync(int userId, int matchedUserId)
        {
            var likes = await _context.Likes.Where(l =>
                (l.LikerId == userId && l.LikedUserId == matchedUserId) ||
                (l.LikerId == matchedUserId && l.LikedUserId == userId)).ToListAsync();

            if (likes.Any())
            {
                _context.Likes.RemoveRange(likes);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException(nameof(Like), userId, matchedUserId);
            }
        }

        //Helper Methods
        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.Likes.AnyAsync(c => c.Id == id);
        }
    }
}







