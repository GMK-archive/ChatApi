namespace WebApplication9
{
    public class ChatHistory
    {
        Database db;
        public List<ChatMessage> Messages { get; set; }

        public ChatHistory(Database db)
        {
            Messages = new List<ChatMessage>();
            this.db = db;
        }
        public void AddMessage(ChatMessage message)
        {
            //Messages.Add(message);
            db.Messages.Add(message);
            db.SaveChanges();
        }
        public List<ChatMessage> GetMessagesAfter(DateTime timestamp)
        {
            return db.Messages.Where(m => m.Timestamp >= timestamp).ToList();
        }
        public List<ChatMessage> GetLast(int count)
        {
            return db.Messages.OrderByDescending(m => m.Timestamp).Take(count).OrderBy(m => m.Timestamp).ToList();
        }
    }
}
