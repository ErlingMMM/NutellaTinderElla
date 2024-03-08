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



        public async Task<IEnumerable<int>> GetUsersMatchByUsersIdsAsync(int id)
        {
            var matchesAsMatcher = await _context.Matches
                .Where(m => m.MacherId == id)
                .Select(m => m.MatchedUserId)
                .ToListAsync();

            var matchesAsMatchedUser = await _context.Matches
                .Where(m => m.MatchedUserId == id)
                .Select(m => m.MacherId)
                .ToListAsync();

            // Combine and return all matched user IDs
            var allMatches = matchesAsMatcher.Union(matchesAsMatchedUser);

            return allMatches;
        }


        public async Task<IEnumerable<int>> GetSwipedUserIdsAsync(int swipingUserId)
        {
            // Query the Swipes table to get swiped user IDs for the given user
            var swipedUserIds = await _context.Swipes
                .Where(s => s.SwiperId == swipingUserId)
                .Select(s => s.SwipedUserId)
                .ToListAsync();

            return swipedUserIds;
        }


        //Helper Methods
        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
        }

    }
}

