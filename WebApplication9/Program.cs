using WebApplication9;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

ChatHistory chatHistory = new ChatHistory();
chatHistory.AddMessage(new ChatMessage("User1", "Hello!"));
chatHistory.AddMessage(new ChatMessage("User2", "Deez Nuts"));
chatHistory.AddMessage(new ChatMessage("User3", "67"));

app.MapGet("/", () => "Hello World!");
app.MapGet("/chat", (DateTime? timestamp) =>
{
    if (timestamp == null)
        return chatHistory.GetLast(10);

    return chatHistory.GetMessagesAfter(timestamp ?? DateTime.Now);
});

app.Run();
