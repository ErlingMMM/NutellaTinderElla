using NutellaTinderElla.Data.Models;
using NutellaTinderEllaApi.Data.Models;
using NutellaTinderEllaApi.Services;

namespace NutellaTinderElla.Services.Messaging

{
    public interface IMessageService : ICrudService<Message, int>
    {
        Task<IEnumerable<Message>> GetUsersMessagesByUsersIdsAsync(int senderId, int receiverId);
        Task<IEnumerable<Message>> UpdateMessagesToRead(int senderId, int receiverId);
        Task SendMessageAsync(int senderId, int receiverId, string content);
    }
}



