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

// logowanie
app.MapPost("/user/me", async (Database db, User user) =>
{
    return Results.Ok();
});
// rejestracja
app.MapPost("/users", (Database db, User user) =>
{
    db.Users.Add(user);
    db.SaveChanges();
    return Results.Created($"{user.Id}", user);
});
// pobranie histori chatu
app.MapGet("/chat/messages", (Database db, string minimalDate) =>
{
    ChatHistory chatHistory = new ChatHistory(db);
    DateTime parsedTimestamp = DateTime.Now;
    DateTime.TryParse(minimalDate, out parsedTimestamp);
    return chatHistory.GetMessagesAfter(parsedTimestamp);
});
app.MapPost("/chat/messages", (Database db, ChatMessage message) =>
{
    if (string.IsNullOrEmpty(message.Content) || string.IsNullOrEmpty(message.Autor))
    {
        return Results.BadRequest("Author or Content is empty. Author or Content fields are requied");
    }
    ChatHistory chatHistory = new ChatHistory(db);
    chatHistory.AddMessage(message);
    return Results.Created($"/chat/messages/{message.Timestamp.Ticks}", message);
});

app.Run();
