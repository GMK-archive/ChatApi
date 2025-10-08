namespace WebApplication9
{
    public class ChatMessage
    {
        public string Autor { get; set; }
        public string Content { get; set; }
        public DateTime Timestamp { get; set; }

        public ChatMessage(string autor, string content)
        {
            Autor = autor;
            Content = content;
            Timestamp = DateTime.Now;
        }
    }
}
