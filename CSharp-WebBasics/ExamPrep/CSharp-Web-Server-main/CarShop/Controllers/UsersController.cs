using CarShop.Services;
using CarShop.ViewModels;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class UsersController : Controller
    {
        private readonly IValidator validator;
        private readonly IUsersService usersService;

        public UsersController(IValidator validator, IUsersService usersService)
        {
            this.validator = validator;
            this.usersService = usersService;
        }

        public HttpResponse Register() => View();

        [HttpPost]
        public HttpResponse Register(RegisterUserViewModel model)
        {
            var errors = this.validator.ValidateUser(model);

            if (!this.usersService.IsUsernameAvailable(model.Username))
            {
                errors.Add($"The username '{model.Username}' is already registered.");
            }

            if (!this.usersService.IsEmailAvailable(model.Email))
            {
                errors.Add($"The email '{model.Email}' is already registered.");
            }

            if (errors.Any())
            {
                return Error(errors);
            }

            this.usersService.Create(model.Username, model.Email, model.Password, model.UserType);

            return Redirect("/Users/Login");
        }

        public HttpResponse Login() => View();

        [HttpPost]
        public HttpResponse Login(LoginUserViewModel model)
        {
            var user = this.usersService.GetUserId(model.Username, model.Password);

            if (user == null)
            {
                return Error("Invalid username or password");
            }

            this.SignIn(user);

            return Redirect("/Cars/All");
        }

        [Authorize]
        public HttpResponse Logout()
        {
            this.SignOut();
            return Redirect("/");
        }
    }
}
