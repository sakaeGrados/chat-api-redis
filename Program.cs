using BDII_TP.Hubs;
using BDII_TP.Repositories;
using BDII_TP.Services.Implementations;
using BDII_TP.Services.Interfaces;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMessageRepository, MessageRepository>();
builder.Services.AddScoped<IMessageService, MessageService>();
builder.Services.AddSignalR();
builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
{
    var config = new ConfigurationOptions
    {
        EndPoints = { { "redis-10143.c308.sa-east-1-1.ec2.cloud.redislabs.com", 10143 } },
        User = "default",
        Password = "mdcWUHKmUGrUGl6rS6UchlYVzEyXjyUn"
    };

    return ConnectionMultiplexer.Connect(config);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");
app.Run();