using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.Messaging

{
    public interface IMessageService : ICrudService<Message, int>
    {
        Task<IEnumerable<Message>> UpdateMessagesToRead(int senderId, int receiverId);
        Task<IEnumerable<Message>> GetFilteredMessagesForTwoUsersAsync(int userId, int matchingUserId);
        Task SendMessageAsync(int senderId, int receiverId, string content);
        Task DeleteMessagesById(int userId, int matchedUserId);
    }
}



