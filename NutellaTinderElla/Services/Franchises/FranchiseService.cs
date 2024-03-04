using WebMovieApi.Data;
using WebMovieApi.Data.Models;
using Microsoft.EntityFrameworkCore;
using WebMovieApi.Data.Exceptions;
using WebMovieApi.Services;


namespace WebMovieApi.Services.Franchises
{
    public class FranchiseService : IFranchiseService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly MovieDbContext _context;

        public FranchiseService(MovieDbContext context)
        {
            _context = context;
        }


        public async Task<ICollection<Franchise>> GetAllAsync()
        {
            return await _context.Franchises.ToListAsync();
        }


        public async Task<Franchise> GetByIdAsync(int id)
        {
            var fra = await _context.Franchises.Where(f => f.Id == id).FirstAsync();

            if (fra is null)
                throw new EntityNotFoundException(nameof(fra), id);

            return fra;
        }
        public async Task<Franchise> AddAsync(Franchise obj) 
        {
            await _context.Franchises.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id) 
        {
            if (!await FranchiseExistsAsync(id))
                throw new EntityNotFoundException(nameof(Franchise), id);

            var fran = await _context.Franchises
                .Where(f => f.Id == id)
                .FirstAsync();

            _context.Franchises.Remove(fran);
            await _context.SaveChangesAsync();
        }

        public async Task<Franchise> UpdateAsync(Franchise obj) 
        {
            if (!await FranchiseExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Franchise), obj.Id);

            _context.Entry(obj).State = EntityState.Modified;
            _context.SaveChanges();

            return obj;
        }

        //From IFranchiseService - double check
        public async Task<ICollection<Movie>> GetAllMoviesInFranchiseAsync(int id) 
        {
            if (!await FranchiseExistsAsync(id))
                throw new EntityNotFoundException(nameof(Franchise), id);

            return await _context.Movies
                .Where(m => m.FranchiseId == id)
                .ToListAsync();

        }
        public async Task UpdateMoviesInFranchiseAsync(int franchiseId, int[] movieIds) 
        {
            if (!await FranchiseExistsAsync(franchiseId))
                throw new EntityNotFoundException(nameof(Franchise), franchiseId);

            var franchise = await _context.Franchises
                .Include(f => f.Movies)
                .FirstOrDefaultAsync(f => f.Id == franchiseId);
            
            if (franchise is null)
                throw new EntityNotFoundException(nameof(Franchise), franchiseId);

            franchise.Movies.Clear();
            foreach (int id in movieIds)
            {
                var movie = await _context.Movies.FirstOrDefaultAsync(m => m.Id == id);
                if (movie != null)
                {
                    franchise.Movies.Add(movie);
                }
            }

            await _context.SaveChangesAsync();
        }
        //Check if franchise exists, then movies, then characters in the movies
        public async Task<ICollection<Character>> GetAllCharactersInFranchiseAsync(int id)
        {
            {
                if (!await FranchiseExistsAsync(id))
                    throw new EntityNotFoundException(nameof(Franchise), id);

                var charactersInFranchise = await _context.Movies
                    .Where(m => m.FranchiseId == id)
                    .SelectMany(m => m.Characters)
                    .Distinct()
                    .ToListAsync();

                Console.WriteLine($"Number of characters in franchise with ID {id}: {charactersInFranchise.Count}");

                return charactersInFranchise;
            }
        }

        //Helper Methods
        private async Task<bool> FranchiseExistsAsync(int id)
        {
            return await _context.Franchises.AnyAsync(f => f.Id == id);
        }
        private Task<bool> CharacterExistsAsync(int characterId)
        {
            return _context.Characters.AnyAsync(c => c.Id == characterId);
        }
        private Task<bool> MovieExistsAsync(int movieId)
        {
            return _context.Movies.AnyAsync(m => m.Id == movieId);
        }
    }
}
