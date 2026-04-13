namespace BDII_TP.Repositories
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync();

        Task<List<Message>> GetMessagesBySenderAsync(string userId);
        Task<List<Message>> GetMessagesByReceiverAsync(string receiverId);
        Task<List<Message>> GetConversationAsync(string user1, string user2);
    }
}
