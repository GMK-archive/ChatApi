using Microsoft.EntityFrameworkCore;
namespace WebApplication9
{
    public class Database : DbContext
    {
        public DbSet<ChatMessage> Messages { get; set; }

        public DbSet<User> Users { get; set; }
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

            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().Property(u => u.Email).IsRequired();
            modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        }
    }
}
