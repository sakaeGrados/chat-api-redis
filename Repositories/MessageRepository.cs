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
}
