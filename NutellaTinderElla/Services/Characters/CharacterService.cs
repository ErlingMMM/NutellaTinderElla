using Microsoft.EntityFrameworkCore;
using WebMovieApi.Data;
using WebMovieApi.Data.Exceptions;
using WebMovieApi.Data.Models;

namespace WebMovieApi.Services.Characters
{
    public class CharacterService : ICharacterService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly MovieDbContext _context;

        public CharacterService(MovieDbContext context)
        {
            _context = context;
        }


        public async Task<ICollection<Character>> GetAllAsync() {
            return await _context.Characters.Include(c => c.Movies).ToListAsync();
          }
        public async Task<Character> GetByIdAsync(int id) 
        {
            var cha = await _context.Characters.Where(c => c.Id == id).FirstAsync();

            if (cha is null)
                throw new EntityNotFoundException(nameof(cha), id);

            return cha;
        }
        public async Task<Character> AddAsync(Character obj) 
        {
            await _context.Characters.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id) 
        {
            if (!await CharacterExistsAsync(id))
                throw new EntityNotFoundException(nameof(Character), id);

            var cha = await _context.Characters
                .Where(c => c.Id == id)
                .FirstAsync();

            //Needs to clear relations
            //cha.Movies.Clear();

            _context.Characters.Remove(cha);
            await _context.SaveChangesAsync();
        }
        public async Task<Character> UpdateAsync(Character obj)
        {
            {
                if (!await CharacterExistsAsync(obj.Id))
                    throw new EntityNotFoundException(nameof(Character), obj.Id);

                //Needs to clear relations
                //obj.Movies.Clear();

                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();

                return obj;
            }
        }

        //Helper Methods
        private async Task<bool> CharacterExistsAsync(int id)
        {
            return await _context.Characters.AnyAsync(c => c.Id == id);
        }
        private Task<bool> FranchiseExistsAsync(int franchiseId)
        {
            return _context.Franchises.AnyAsync(f => f.Id == franchiseId);
        }
        private Task<bool> MovieExistsAsync(int movieId)
        {
            return _context.Movies.AnyAsync(m => m.Id == movieId);
        }
    }
}
