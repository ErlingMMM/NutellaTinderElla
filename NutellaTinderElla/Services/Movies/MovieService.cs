using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data.Exceptions;

namespace NutellaTinderEllaApi.Services.Movies
{
    public class MovieService : IMovieService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly TinderDbContext _context;

        public MovieService(TinderDbContext context)
        {
            _context = context;
        }
        
        //ICrudService
        public async Task<ICollection<Movie>> GetAllAsync() {
            return await _context.Movies.ToListAsync();
        }
        public async Task<Movie> GetByIdAsync(int id)
        {
            var mov = await _context.Movies.Where(m => m.Id == id).FirstAsync();

            if (mov is null)
                throw new EntityNotFoundException(nameof(mov), id);

            return mov;
        }
        public async Task<Movie> AddAsync(Movie obj) 
        {
            await _context.Movies.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id) 
        {
            if (!await MovieExistsAsync(id))
                throw new EntityNotFoundException(nameof(Movie), id);

            var mov = await _context.Movies
                .Where(m => m.Id == id)
                .FirstAsync();

            //Needs to clear relations
            //mov.Characters.Clear();

            _context.Movies.Remove(mov);
            await _context.SaveChangesAsync();
        }
        public async Task<Movie> UpdateAsync(Movie obj) 
        {
            if (!await MovieExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Movie), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        //IMovieService - double check 
        public async Task<ICollection<Character>> GetAllCharactersInMovieAsync(int id) 
        {
            if (!await MovieExistsAsync(id))
                throw new EntityNotFoundException(nameof(Movie), id);

            return await _context.Characters
                .Where(c => c.Movies.Any(m => m.Id == id))
                .ToListAsync();
        }
        public async Task UpdateCharactersInMovieAsync(int movieId, int[] characterIds) 
        {
            if (!await MovieExistsAsync(movieId))
                throw new EntityNotFoundException(nameof(Movie), movieId);

            var movie = await _context.Movies
                .Include(m => m.Characters)
                .FirstOrDefaultAsync(m => m.Id == movieId);

            if (movie is null)
                throw new EntityNotFoundException(nameof(Movie), movieId);

            movie.Characters.Clear();

            foreach (int id in characterIds)
            {
                var character = await _context.Characters.FirstOrDefaultAsync(c => c.Id == id);
                if (character != null)
                {
                    movie.Characters.Add(character);
                }
            }

            await _context.SaveChangesAsync();
        }

        //Helper methods
        private async Task<bool> MovieExistsAsync(int id)
        {
            return await _context.Movies.AnyAsync(m => m.Id == id);
        }
        private Task<bool> CharacterExistsAsync(int characterId)
        {
            return _context.Characters.AnyAsync(c => c.Id == characterId);
        }
        private Task<bool> FranchiseExistsAsync(int franchiseId)
        {
            return _context.Franchises.AnyAsync(f => f.Id == franchiseId);
        }
    }
}
