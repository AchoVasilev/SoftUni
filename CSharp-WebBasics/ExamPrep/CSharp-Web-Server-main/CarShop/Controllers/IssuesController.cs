using CarShop.Services;
using CarShop.ViewModels;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace CarShop.Controllers
{
    public class IssuesController : Controller
    {
        private readonly IIssuesService issuesService;
        private readonly IUsersService usersService;
        private readonly IValidator validator;

        public IssuesController(IIssuesService issuesService, IUsersService usersService, IValidator validator)
        {
            this.issuesService = issuesService;
            this.usersService = usersService;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse CarIssues(string carId)
        {
            var car = this.issuesService.GetAllIssues(carId);

            car.UserIsMechanic = this.usersService.IsUserMechanic(this.User.Id);

            if (car == null)
            {
                return Error($"Car with ID '{carId}' does not exist.");
            }

            return View(car);
        }

        [Authorize]
        public HttpResponse Add(string carId) => View(new AddIssueViewModel { CarId = carId });

        [Authorize]
        [HttpPost]
        public HttpResponse Add(AddIssueFormModel model)
        {
            var errors = this.validator.ValidateIssue(model);

            if (errors.Any())
            {
                return Error(errors);
            }

            this.issuesService.AddIssue(model.CarId, model.Description);

            return Redirect($"/Issues/CarIssues?carId={model.CarId}");
        }

        [Authorize]
        public HttpResponse Delete(string issueId, string carId)
        {
            var userIsMechanic = this.usersService.IsUserMechanic(this.User.Id);

            if (!userIsMechanic)
            {
                var userOwnsCar = this.usersService.UserOwnsCar(this.User.Id, carId);

                if (!userOwnsCar)
                {
                    return Unauthorized();
                }
            }

            this.issuesService.Delete(issueId, carId);

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }

        [Authorize]
        public HttpResponse Fix(string issueId, string carId)
        {
            var userIsMechanic = this.usersService.IsUserMechanic(this.User.Id);

            if (!userIsMechanic)
            {
                return Unauthorized();
            }

            this.issuesService.Fix(issueId);

            return Redirect($"/Issues/CarIssues?carId={carId}");
        }
    }
}
