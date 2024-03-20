using Microsoft.EntityFrameworkCore;
using NutellaTinderEllaApi.Data.Exceptions;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Data;
using NutellaTinderElla.Data.Models;
using NutellaTinderElla.Services.Encryption;

namespace NutellaTinderElla.Services.Messaging
{
    public class MessageService : IMessageService
    {

        private readonly TinderDbContext _context;
        private readonly AesEncryptionService _encryptionService;


        public MessageService(TinderDbContext context, AesEncryptionService encryptionService)
        {
            _context = context;
            _encryptionService = encryptionService;
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
            byte[] iv;
            obj.Content = _encryptionService.Encrypt(obj.Content, out iv);
            obj.IV = iv;

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


        public async Task DeleteMessagesById(int userId, int matchedUserId)
        {
            var message = await _context.Message.Where(m =>
                (m.SenderId == userId && m.ReceiverId == matchedUserId) ||
                (m.SenderId == matchedUserId && m.ReceiverId == userId)).ToListAsync();

            if (message.Any())
            {
                _context.Message.RemoveRange(message);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new EntityNotFoundException(nameof(Like), userId, matchedUserId);
            }
        }

        public async Task<Message> UpdateAsync(Message obj)
        {
            if (!await MessageExistsAsync(obj.Id))
                throw new EntityNotFoundException(nameof(Message), obj.Id);


            _context.Entry(obj).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return obj;
        }






        public async Task<IEnumerable<Message>> UpdateMessagesToRead(int senderId, int receiverId)
        {
            var messagesToUpdate = await _context.Message
                .Where(m => m.SenderId == receiverId && m.ReceiverId == senderId && !m.IsViewed)
                .ToListAsync();

            foreach (var message in messagesToUpdate)
            {
                message.IsViewed = true;
                message.TimeViewed = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();

            return messagesToUpdate;
        }



        public async Task SendMessageAsync(int senderId, int receiverId, string content, byte[] iv)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                IV = iv,
                Timestamp = DateTime.UtcNow
            };

            _context.Message.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Message>> GetFilteredMessagesForTwoUsersAsync(int userId, int matchingUserId)
        {
            var allMessages = await GetAllAsync();

            var filteredMessages = allMessages.Where(m =>
                (m.SenderId == userId && m.ReceiverId == matchingUserId) ||
                (m.SenderId == matchingUserId && m.ReceiverId == userId)
            ).OrderByDescending(m => m.Timestamp).ToList();


            return filteredMessages;
        }

        //Helper Methods
        private async Task<bool> MessageExistsAsync(int id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
        }

    }
}


