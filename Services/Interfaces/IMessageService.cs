using BDII_TP.DTOs;

namespace BDII_TP.Services.Interfaces
{
    public interface IMessageService
    {
        Task<Message> CreateMessageAsync(CreateMessageDto dto);
        Task<List<Message>> GetMessagesAsync();
    }
}