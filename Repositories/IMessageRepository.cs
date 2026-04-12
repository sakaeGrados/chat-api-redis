namespace BDII_TP.Repositories
{
    public interface IMessageRepository
    {
        Task SaveMessageAsync(Message message);
        Task<List<Message>> GetMessagesAsync();
    }
}
