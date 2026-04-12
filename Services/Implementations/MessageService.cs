using BDII_TP.DTOs;
using BDII_TP.Hubs;
using BDII_TP.Repositories;
using BDII_TP.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace BDII_TP.Services.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly IMessageRepository _repository;
        private readonly IHubContext<ChatHub> _hub;
        public MessageService(IMessageRepository repository)
        {
            _repository = repository;
        }

        public MessageService(IMessageRepository repository, IHubContext<ChatHub> hub)
        {
            _repository = repository;
            _hub = hub;
        }
        public async Task<Message> CreateMessageAsync(CreateMessageDto dto)
        {
            var message = new Message
            {
                Id = Guid.NewGuid().ToString(),
                UserId = dto.UserId,
                Username = dto.Username,
                Content = dto.Content,
                Timestamp = DateTime.UtcNow
            };

            await _repository.SaveMessageAsync(message);

            await _hub.Clients.All.SendAsync("ReceiveMessage", message);

            return message;
        }

        public async Task<List<Message>> GetMessagesAsync()
        {
            return await _repository.GetMessagesAsync();
        }
    }
}
