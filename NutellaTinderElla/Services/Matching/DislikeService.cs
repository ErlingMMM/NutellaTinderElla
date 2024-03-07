using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;
using NutellaTinderElla.Data.Models;

namespace NutellaTinderElla.Services.Matching
{
    public class DislikeService : IDislikeService
    {
        private readonly TinderDbContext _context;

        public DislikeService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Dislike>> GetAllAsync()
        {
            return await _context.Dislikes.ToListAsync();
        }

        public async Task<Dislike> GetByIdAsync(int id)
        {
            var curUs = await _context.Dislikes.Where(c => c.Id == id).FirstAsync();

            if (curUs is null)
                throw new EntityNotFoundException(nameof(curUs), id);

            return curUs;
        }

        public async Task<Dislike> AddAsync(Dislike obj)
        {
            await _context.Dislikes.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            if (!await DislikeExistsAsync(id))
                throw new EntityNotFoundException(nameof(Dislike), id);

            var curUs = await _context.Dislikes
                .Where(c => c.Id == id)
                .FirstAsync();

            _context.Dislikes.Remove(curUs);
            await _context.SaveChangesAsync();
        }

        public async Task<Dislike> UpdateAsync(Dislike obj)
        {
            if (!await DislikeExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Dislike), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }

        // Helper Methods
        private async Task<bool> DislikeExistsAsync(int id)
        {
            return await _context.Dislikes.AnyAsync(c => c.Id == id);
        }
    }
}
