using Microsoft.EntityFrameworkCore;
namespace WebApplication9
{
    public class Database : DbContext
    {
        public DbSet<ChatMessage> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=chat.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ChatMessage>().ToTable("Messages");
            modelBuilder.Entity<ChatMessage>().HasKey(m => m.Timestamp);
            modelBuilder.Entity<ChatMessage>().Property(m => m.Autor).IsRequired();
            modelBuilder.Entity<ChatMessage>().Property(m => m.Content).IsRequired();
        }
    }
}
