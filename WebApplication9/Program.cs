using System.Web;
using WebApplication9;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ChatHistory chatHistory = new ChatHistory();
chatHistory.AddMessage(new ChatMessage("User1", "Hello!"));
chatHistory.AddMessage(new ChatMessage("User2", "Deez Nuts"));
chatHistory.AddMessage(new ChatMessage("User3", "67"));

app.MapGet("/", () => "Hello World!");
app.MapGet("/chat", (string? timestamp) =>
{
    if (timestamp == null)
        return chatHistory.GetLast(10);
    DateTime parsedTimestamp = DateTime.Now;
    DateTime.TryParse(timestamp, out parsedTimestamp);
    return chatHistory.GetMessagesAfter(parsedTimestamp);
});
app.MapPost("/chat", (ChatMessage message) =>
{
    chatHistory.AddMessage(message);
    return Results.Created($"/chat/{message.Timestamp.Ticks}", message);
});

app.Run();
