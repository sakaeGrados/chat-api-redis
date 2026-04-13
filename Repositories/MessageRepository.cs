namespace BDII_TP.Repositories;
using StackExchange.Redis;
using System.Text.Json;

public class MessageRepository : IMessageRepository
{
    private readonly IDatabase _database;
    private const string KEY = "chat:messages";

    public MessageRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task SaveMessageAsync(Message message)
    {
        var json = JsonSerializer.Serialize(message);
        await _database.ListRightPushAsync(KEY, json);
    }

    public async Task<List<Message>> GetMessagesAsync()
    {
        var values = await _database.ListRangeAsync(KEY);

        return values
    .Select(v => JsonSerializer.Deserialize<Message>(v.ToString()))
    .Where(m => m != null)
    .Select(m => m!)
    .ToList();
    }

    public async Task<List<Message>> GetMessagesBySenderAsync(string userId)
    {
        var messages = await GetMessagesAsync();

        return messages
            .Where(m => m.UserId == userId)
            .ToList();
    }

    public async Task<List<Message>> GetMessagesByReceiverAsync(string receiverId)
    {
        var messages = await GetMessagesAsync();

        return messages
            .Where(m => m.ReceiverId == receiverId)
            .ToList();
    }

    public async Task<List<Message>> GetConversationAsync(string user1, string user2)
    {
        var messages = await GetMessagesAsync();

        return messages
            .Where(m =>
                (m.UserId == user1 && m.ReceiverId == user2) ||
                (m.UserId == user2 && m.ReceiverId == user1)
            )
            .OrderBy(m => m.Timestamp)
            .ToList();
    }
}
