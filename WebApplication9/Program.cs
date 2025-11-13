using Isopoh.Cryptography.Argon2;
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
app.MapPost("/user/me", (Database db, User user) =>
{
    if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.PasswordHash))
    {
        return Results.BadRequest("Error Account is not found");
    }
    var foundUser = db.Users.Where(u => u.Email == user.Email).FirstOrDefault();
    if(foundUser == null)
    {
        return Results.Unauthorized();
    }
    bool paswordOk = Argon2.Verify(foundUser.PasswordHash, user.PasswordHash);

    if (!paswordOk)
    {
        return Results.Unauthorized();
    }
    string token = "superTajnyToken";
    return Results.Ok(token);
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
