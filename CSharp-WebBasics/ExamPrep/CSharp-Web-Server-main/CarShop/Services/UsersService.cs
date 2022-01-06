using CarShop.Data;
using CarShop.Data.Models;
using System.Linq;
using static CarShop.Data.DataConstants;

namespace CarShop.Services
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

        public void Create(string username, string email, string password, string userType)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = this.passwordHasher.HashPassword(password),
                IsMechanic = userType == UserIsMechanic
            };

            this.data.Users.Add(user);
            this.data.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user = this.data.Users.FirstOrDefault(x => x.Username == username && x.Password == this.passwordHasher.HashPassword(password));

            return user?.Id;
        }

        public bool IsUserMechanic(string userId)
        {
            var user = this.data.Users.FirstOrDefault(x => x.Id == userId);

            return user.IsMechanic;
        }

        public bool IsUsernameAvailable(string username)
            => !this.data.Users.Any(x => x.Username == username);

        public bool IsEmailAvailable(string email)
            => !this.data.Users.Any(x => x.Email == email);

        public bool UserOwnsCar(string userId, string carId)
            => this.data.Cars
                    .Any(x => x.OwnerId == userId && x.Id == carId);
    }
}
