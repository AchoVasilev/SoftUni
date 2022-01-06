using System.Linq;
using Git.Data;

namespace Git.Services
{
    public class UsersService : IUsersService
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IPasswordHasher passwordHasher;

        public UsersService(ApplicationDbContext dbContext, IPasswordHasher passwordHasher)
        {
            this.dbContext = dbContext;
            this.passwordHasher = passwordHasher;
        }

        public string CreateUser(string username, string email, string password)
        {
          
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = this.passwordHasher.HashPassword(password)
            };

            this.dbContext.Users.Add(user);

            dbContext.SaveChanges();

            return user.Id;
        }

        public string GetUserId(string username, string password)
        {
            var user = this.dbContext.Users
                           .FirstOrDefault(x => x.Username == username && x.Password == passwordHasher.HashPassword(password));

            return user?.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            if (this.dbContext.Users.Any(x => x.Email == email))
            {
                return false;
            }

            return true;
        }

        public bool IsUsernameAvailable(string username)
        {
            if (this.dbContext.Users.Any(x => x.Username == username))
            {
                return false;
            }

            return true;
        }
    }
}
