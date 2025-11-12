using Isopoh.Cryptography.Argon2;

namespace WebApplication9
{
    public class User
    {

        public int Id { get; set; }

        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public User(string email, string passwordHash)
        {
            //unikatowe id
            Id = Guid.NewGuid().GetHashCode();
            //login i email
            Email = email;
            //password hash argon2i z passwordem
            PasswordHash = passwordHash;
        }
        
    }
}
