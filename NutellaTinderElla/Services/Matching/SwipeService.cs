using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;
using NutellaTinderElla.Data.Models;

namespace NutellaTinderElla.Services.Matching
{
    public class SwipeService : ISwipeService
    {
        private readonly TinderDbContext _context;

        public SwipeService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Swipes>> GetAllAsync()
        {
            return await _context.Swipes.ToListAsync();
        }

        public async Task<Swipes> GetByIdAsync(int id)
        {
            var curUs = await _context.Swipes.Where(c => c.Id == id).FirstAsync();

            if (curUs is null)
                throw new EntityNotFoundException(nameof(curUs), id);

            return curUs;
        }

        public async Task<Swipes> AddAsync(Swipes obj)
        {
            await _context.Swipes.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }




        public async Task DeleteByIdAsync(int id)
        {
            var swipe = await _context.Swipes.Where(l =>
                      (l.SwiperId == id || l.SwipedUserId == id)).ToListAsync();

            if (swipe.Any())
            {
                _context.Swipes.RemoveRange(swipe);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteSwipeByIdAsync(int userId, int matchedUserId)
        {
            var swipes = await _context.Swipes.Where(s =>
                (s.SwiperId == userId && s.SwipedUserId == matchedUserId) ||
                (s.SwiperId == matchedUserId && s.SwipedUserId == userId)).ToListAsync();

            if (swipes.Any())
            {
                _context.Swipes.RemoveRange(swipes);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException(nameof(Swipes), userId, matchedUserId);
            }
        }


        public async Task<Swipes> UpdateAsync(Swipes obj)
        {
            if (!await SwipeExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Swipes), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        // Helper Methods
        private async Task<bool> SwipeExistsAsync(int id)
        {
            return await _context.Swipes.AnyAsync(c => c.Id == id);
        }
    }
}
