using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data;
using NutellaTinderElla.Data.Models;


namespace NutellaTinderElla.Services.Matching
{
    public class MatchService : IMatchService
    {
        private readonly TinderDbContext _context;

        public MatchService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Match>> GetAllAsync()
        {
            return await _context.Matches.ToListAsync();
        }

        public async Task<Match> GetByIdAsync(int id)
        {
            return await _context.Matches.FindAsync(id) ?? throw new EntityNotFoundException(nameof(Match), id);
        }


        public async Task<Match> AddAsync(Match obj)
        {
            await _context.Matches.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }

        public async Task DeleteByIdAsync(int id)
        {
            var match = await _context.Matches.FindAsync(id);
            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException(nameof(Match), id);
            }
        }



        public async Task<Match> UpdateAsync(Match obj)
        {
            if (!_context.Matches.Any(m => m.Id == obj.Id))
                throw new EntityNotFoundException(nameof(Match), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return obj;
        }


        public async Task DeleteMatchByIdAsync(int userId, int matchedUserId)
        {
            var match = await _context.Matches.FirstOrDefaultAsync(m =>
      (m.MacherId == userId && m.MatchedUserId == matchedUserId) ||
      (m.MacherId == matchedUserId && m.MatchedUserId == userId));

            if (match != null)
            {
                _context.Matches.Remove(match);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException(nameof(Match), userId, matchedUserId);
            }
        }


    }
}