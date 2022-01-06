using System.Linq;
using System.Collections.Generic;
using CarShop.Services;
using CarShop.ViewModels;
using MyWebServer.Controllers;
using MyWebServer.Http;

namespace CarShop.Controllers
{
    public class CarsController : Controller
    {
        private readonly IUsersService usersService;
        private readonly ICarsService carsService;
        private readonly IValidator validator;

        public CarsController(IUsersService usersService, ICarsService carsService, IValidator validator)
        {
            this.usersService = usersService;
            this.carsService = carsService;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            ICollection<AllCarsViewModel> cars;

            if (this.usersService.IsUserMechanic(this.User.Id))
            {
                cars = carsService.ListMechanicCars();
            }
            else
            {
                cars = carsService.ListUserCars(this.User.Id);
            }

            return View(cars);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (this.usersService.IsUserMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            return View();
        }

        [HttpPost]
        [Authorize]
        public HttpResponse Add(AddCarViewModel model)
        {
            if (this.usersService.IsUserMechanic(this.User.Id))
            {
                return Unauthorized();
            }

            var errors = this.validator.ValidateCar(model);

            if (errors.Any())
            {
                return Error(errors);
            }

            string carId = this.carsService.AddCar(model.Model, model.Year, model.Image, model.PlateNumber, this.User.Id);

            return Redirect("/Cars/All");
        }
    }
}
