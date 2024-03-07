using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;
using NutellaTinderElla.Data.Models;

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
            if (!await CurrentUserExistsAsync(id))
                throw new EntityNotFoundException(nameof(Like), id);

            var curUs = await _context.CurrentUser
                .Where(c => c.Id == id)
                .FirstAsync();



            _context.CurrentUser.Remove(curUs);
            await _context.SaveChangesAsync();
        }
        public async Task<Like> UpdateAsync(Like obj)
        {
            if (!await CurrentUserExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Like), obj.Id);


            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }


        //Helper Methods
        private async Task<bool> CurrentUserExistsAsync(int id)
        {
            return await _context.Likes.AnyAsync(c => c.Id == id);
        }
    }
}







