namespace BDII_TP.Controllers;
using BDII_TP.DTOs;
using BDII_TP.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMessageService _service;

    public MessageController(IMessageService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateMessageDto dto)
    {
        var message = await _service.CreateMessageAsync(dto);

        var result = new MessageDto
        {
            Id = message.Id,
            Username = message.Username,
            Content = message.Content,
            Timestamp = message.Timestamp
        };

        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var messages = await _service.GetMessagesAsync();

        var result = messages.Select(m => new MessageDto
        {
            Id = m.Id,
            Username = m.Username,
            Content = m.Content,
            Timestamp = m.Timestamp
        });

        return Ok(result);
    }
    [HttpGet("sent/{userId}")]
    public async Task<IActionResult> GetSent(string userId)
    {
        var result = await _service.GetMessagesBySenderAsync(userId);
        return Ok(result);
    }
    [HttpGet("received/{userId}")]
    public async Task<IActionResult> GetReceived(string userId)
    {
        var result = await _service.GetMessagesByReceiverAsync(userId);
        return Ok(result);
    }
    [HttpGet("conversation")]
    public async Task<IActionResult> GetConversation(string user1, string user2)
    {
        var result = await _service.GetConversationAsync(user1, user2);
        return Ok(result);
    }
}
