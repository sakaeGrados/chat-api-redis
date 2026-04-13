using BDII_TP.DTOs;

namespace BDII_TP.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessageAsync(CreateMessageDto dto);
        Task<List<Message>> GetMessagesAsync();
        Task<List<Message>> GetMessagesBySenderAsync(string userId);
        Task<List<Message>> GetMessagesByReceiverAsync(string receiverId);
        Task<List<Message>> GetConversationAsync(string user1, string user2);
    }
}