using System.Web;
using WebApplication9;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Database>();

var app = builder.Build();
app.UseBearerMiddleware();

//ChatHistory chatHistory = new ChatHistory();
//chatHistory.AddMessage(new ChatMessage("User1", "Hello!"));
//chatHistory.AddMessage(new ChatMessage("User2", "Deez Nuts"));
//chatHistory.AddMessage(new ChatMessage("User3", "67"));
//timestamp jest w postaci stringa "2025-10-13T13:08:22.1712280+02:00"

app.MapGet("/", () => "Hello World!");

app.MapGet("/login", async () =>
{
    return Results.Ok();
});

app.MapGet("/chat", (Database db, string? timestamp) =>
{
    ChatHistory chatHistory = new ChatHistory(db);
    if (timestamp == null)
        return chatHistory.GetLast(10);
    DateTime parsedTimestamp = DateTime.Now;
    DateTime.TryParse(timestamp, out parsedTimestamp);
    return chatHistory.GetMessagesAfter(parsedTimestamp);
});
app.MapPost("/chat", (Database db, ChatMessage message) =>
{
    if(message.Content == null || message.Content == string.Empty || message.Autor == null || message.Autor == string.Empty)
    {
        return Results.BadRequest("Author or Content is empty. Author or Content fields are requied");
    }
    ChatHistory chatHistory = new ChatHistory(db);
    chatHistory.AddMessage(message);
    return Results.Created($"/chat/{message.Timestamp.Ticks}", message);
});

app.Run();
