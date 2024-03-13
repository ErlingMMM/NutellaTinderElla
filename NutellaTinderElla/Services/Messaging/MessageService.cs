using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;

namespace NutellaTinderElla.Services.Messaging
{
    public class MessageService : IMessageService
    {

        private readonly TinderDbContext _context;

        public MessageService(TinderDbContext context)
        {
            _context = context;
        }

        public async Task<ICollection<Message>> GetAllAsync()
        {
            return await _context.Message.ToListAsync();
        }

        public async Task<Message> GetByIdAsync(int id)
        {
            var message = await _context.Message.Where(c => c.Id == id).FirstAsync();

            if (message is null)
                throw new EntityNotFoundException(nameof(message), id);

            return message;
        }
        public async Task<Message> AddAsync(Message obj)
        {
            await _context.Message.AddAsync(obj);
            await _context.SaveChangesAsync();
            return obj;
        }
        public async Task DeleteByIdAsync(int id)
        {

            var messages = await _context.Message.Where(m =>
                       (m.SenderId == id || m.ReceiverId == id)).ToListAsync();

            if (messages.Any())
            {
                _context.Message.RemoveRange(messages);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Message> UpdateAsync(Message obj)
        {
            if (!await UserExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Message), obj.Id);


            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }



        public async Task<IEnumerable<Message>> GetUsersMessagesByUsersIdsAsync(int senderId, int receiverId)
        {
            var messages = await _context.Message
                .Where(m => (m.SenderId == senderId && m.ReceiverId == receiverId) ||
                            (m.ReceiverId == senderId && m.SenderId == receiverId))
                .ToListAsync();

            return messages;
        }


        public async Task<IEnumerable<Message>> UpdateMessagesToRead(int senderId, int receiverId)
        {
            var messagesToUpdate = await _context.Message
                .Where(m => m.SenderId == receiverId && m.ReceiverId == senderId && !m.IsViewed)
                .ToListAsync();

            foreach (var message in messagesToUpdate)
            {
                message.IsViewed = true;
            }

            await _context.SaveChangesAsync();

            return messagesToUpdate;
        }





        //Helper Methods
        private async Task<bool> UserExistsAsync(int id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
        }

    }
}


