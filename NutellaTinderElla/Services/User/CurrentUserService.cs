using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;

namespace NutellaTinderElla.Services.ActiveUser

{
    public class CurrentUserService : ICurrentUserService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly TinderDbContext _context;

        public CurrentUserService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<CurrentUser>> GetAllAsync()
        {
            return await _context.CurrentUser.ToListAsync();
        }

        public async Task<CurrentUser> GetByIdAsync(int id)
        {
            var curUs = await _context.CurrentUser.Where(c => c.Id == id).FirstAsync();

            if (curUs is null)
                throw new EntityNotFoundException(nameof(curUs), id);

            return curUs;
        }
        public async Task<CurrentUser> AddAsync(CurrentUser obj)
        {
            await _context.CurrentUser.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id)
        {
            if (!await CurrentUSerExistsAsync(id))
                throw new EntityNotFoundException(nameof(CurrentUser), id);

            var curUs = await _context.CurrentUser
                .Where(c => c.Id == id)
                .FirstAsync();

            //Needs to clear relations
            //cha.Movies.Clear();

            _context.CurrentUser.Remove(curUs);
            await _context.SaveChangesAsync();
        }
        public async Task<CurrentUser> UpdateAsync(CurrentUser obj)
        {
            {
                if (!await CurrentUSerExistsAsync(obj.Id))
                    throw new EntityNotFoundException(nameof(CurrentUser), obj.Id);

                //Needs to clear relations
                //obj.Movies.Clear();

                _context.Entry(obj).State = EntityState.Modified;
                _context.SaveChanges();

                return obj;
            }
        }

        //Helper Methods
        private async Task<bool> CurrentUSerExistsAsync(int id)
        {
            return await _context.CurrentUser.AnyAsync(c => c.Id == id);
        }
        /*  private Task<bool> FranchiseExistsAsync(int franchiseId)
          {
              return _context.Franchises.AnyAsync(f => f.Id == franchiseId);
          }
          private Task<bool> MovieExistsAsync(int movieId)
          {
              return _context.Movies.AnyAsync(m => m.Id == movieId);
          }*/
    }
}

