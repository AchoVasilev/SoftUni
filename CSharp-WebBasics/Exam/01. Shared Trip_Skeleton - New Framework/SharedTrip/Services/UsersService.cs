using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services.Contracts;
using System.Linq;

namespace SharedTrip.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext data;
        private readonly IPasswordHasher passwordHasher;

        public UsersService(ApplicationDbContext data, IPasswordHasher passwordHasher)
        {
            this.data = data;
            this.passwordHasher = passwordHasher;
        }

        public void Create(string username, string email, string password)
        {
                var user = new User()
                {
                    Username = username,
                    Email = email,
                    Password = this.passwordHasher.HashPassword(password)
                };

                this.data.Users.Add(user);
                this.data.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = this.data.Users.FirstOrDefault(x => x.Username == username && x.Password == this.passwordHasher.HashPassword(password));

            return user?.Id;
        }

        public bool IsUsernameAvailable(string username)
            => !this.data.Users.Any(x => x.Username == username);

        public bool IsEmailAvailable(string email)
            => !this.data.Users.Any(x => x.Email == email);
    }
}
