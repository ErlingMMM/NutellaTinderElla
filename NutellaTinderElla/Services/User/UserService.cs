using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;

namespace NutellaTinderElla.Services.ActiveUser

{
    public class UserService : IUserService
    {
        //Handle tasks like data validation, processing, and interactions with the database or external APIs.
        //Ensure that the application's business rules are enforced.

        private readonly TinderDbContext _context;

        public UserService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<User>> GetAllAsync()
        {
            return await _context.User.ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var curUs = await _context.User.Where(c => c.Id == id).FirstAsync();

            if (curUs is null)
                throw new EntityNotFoundException(nameof(curUs), id);

            return curUs;
        }
        public async Task<User> AddAsync(User obj)
        {
            await _context.User.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id)
        {
            if (!await UserExistsAsync(id))
                throw new EntityNotFoundException(nameof(User), id);

            var curUs = await _context.User
                .Where(c => c.Id == id)
                .FirstAsync();

            //Needs to clear relations
            //cha.Movies.Clear();

            _context.User.Remove(curUs);
            await _context.SaveChangesAsync();
        }
        public async Task<User> UpdateAsync(User obj)
        {
            if (!await UserExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(User), obj.Id);


            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }


        //Helper Methods
        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
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

