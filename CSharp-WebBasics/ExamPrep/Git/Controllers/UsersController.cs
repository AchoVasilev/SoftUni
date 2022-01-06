using Git.Services;
using Git.ViewModels.Users;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace Git.Controllers
{
    public class UsersController : Controller
    {
        private readonly IUsersService usersService;
        private readonly IPasswordHasher passwordHasher;
        private readonly IValidator validator;

        public UsersController(IUsersService usersService, IPasswordHasher passwordHasher, IValidator validator)
        {
            this.usersService = usersService;
            this.passwordHasher = passwordHasher;
            this.validator = validator;
        }

        public HttpResponse Register() => this.View();

        [HttpPost]
        public HttpResponse Register(RegisterUserFormModel model)
        {
            var modelErrors = this.validator.ValidateUser(model);

            if (!usersService.IsUsernameAvailable(model.Username))
            {
                modelErrors.Add($"A user with a username {model.Username} already exists.");
            }

            if (!usersService.IsEmailAvailable(model.Email))
            {
                modelErrors.Add($"A user with an email {model.Email} already exists.");
            }

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.usersService.CreateUser(model.Username, model.Email, model.Password);

            return Redirect("/Users/Login");
        }

        public HttpResponse Login() => this.View();

        [HttpPost]
        public HttpResponse Login(LogInUserFormModel model)
        {
            var userId = usersService.GetUserId(model.Username, model.Password);

            if (userId == null)
            {
                return Error("Username and password combination is not valid.");
            }

            this.SignIn(userId);

            return Redirect("/Repositories/All");
        }

        public HttpResponse Logout()
        {
            this.SignOut();

            return Redirect("/");
        }
    }
}
