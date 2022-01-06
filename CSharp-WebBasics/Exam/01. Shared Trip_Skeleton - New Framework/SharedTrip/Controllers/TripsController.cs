using MyWebServer.Controllers;
using MyWebServer.Http;
using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Trips;
using System.Linq;

namespace SharedTrip.Controllers
{
    public class TripsController : Controller
    {
        private readonly ITripsService tripsService;
        private readonly IValidator validator;
        private readonly ApplicationDbContext data;

        public TripsController(ITripsService tripsService, IValidator validator, ApplicationDbContext data)
        {
            this.tripsService = tripsService;
            this.validator = validator;
            this.data = data;
        }

        [Authorize]
        public HttpResponse All()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            var trips = this.tripsService.GetAllTrips();

            return View(trips);
        }

        [Authorize]
        public HttpResponse Add()
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Add(TripsAddViewModel model)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            var errors = this.validator.ValidateTrip(model);

            if (errors.Any())
            {
                return Error(errors);
            }

            this.tripsService.CreateTrip(model.StartPoint, model.EndPoint, model.DepartureTime, model.ImagePath, model.Seats, model.Description);

            return Redirect("/Trips/All");
        }

        [Authorize]
        public HttpResponse Details(string tripId)
        {
            if (!this.User.IsAuthenticated)
            {
                return this.Redirect("/Users/Login");
            }

            var trips = this.tripsService.GetTripDetails(tripId);

            return View(trips);
        }

        [Authorize]
        public HttpResponse AddUserToTrip(string tripId)
        {
            var userId = this.User.Id;

            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(tripId))
            {
                return Error("UserId and tripId can not be null or empty");
            }

            var checkForUserTrip = this.tripsService.CheckForUserTrip(tripId, userId);

            if (checkForUserTrip)
            {
                return Error("You have already joined this trip.");
            }

            this.tripsService.AddUserToTrip(tripId, userId);

            return Redirect("/Trips/All");
        }
    }
}
