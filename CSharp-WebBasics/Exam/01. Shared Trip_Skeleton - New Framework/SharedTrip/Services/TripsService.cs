using SharedTrip.Data;
using SharedTrip.Models;
using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SharedTrip.Services
{
    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext data;

        public TripsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void CreateTrip(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description)
        {
            var date = DateTime.TryParseExact(departureTime, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out var result);

            var trip = new Trip()
            {
                StartPoint = startPoint,
                EndPoint = endPoint,
                DepartureTime = result,
                ImagePath = imagePath,
                Seats = seats,
                Description = description
            };

            this.data.Trips.Add(trip);
            this.data.SaveChanges();
        }

        public IEnumerable<TripListingViewModel> GetAllTrips()
        {
            var trips = this.data.Trips
                .Select(x => new TripListingViewModel
                {
                    Id = x.Id,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToString(),
                    Seats = x.Seats
                })
                .ToList();

            return trips;
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            var trip = this.data.Trips
                .Where(x => x.Id == tripId)
                .FirstOrDefault();

            trip.UserTrips.Add(new UserTrip
            {
                TripId = tripId,
                UserId = userId
            });

            var currentSeats = trip.Seats;

            if (currentSeats != 0)
            {
                trip.Seats = currentSeats - 1;
            }

            this.data.Trips.Update(trip);
            this.data.SaveChanges();
        }

        public TripDetailsViewModel GetTripDetails(string tripId)
        {
            var date = this.data.Trips
                .Where(x => x.Id == tripId)
                .Select(x => x.DepartureTime)
                .FirstOrDefault();

            var trip = this.data.Trips
                .Where(x => x.Id == tripId)
                .Select(x => new TripDetailsViewModel
                {
                    Id = tripId,
                    StartPoint = x.StartPoint,
                    EndPoint = x.EndPoint,
                    DepartureTime = x.DepartureTime.ToString(),
                    Image = x.ImagePath,
                    Seats = x.Seats,
                    Description = x.Description
                })
                .FirstOrDefault();

            return trip;
        }

        public string GetTripId(string tripId)
        {
            var trip = this.data.Trips.FirstOrDefault(x => x.Id == tripId);

            return trip?.Id;
        }

        public bool CheckForUserTrip(string tripId, string userId) 
            => this.data
                .UsersTrips
                .Any(c => c.TripId == tripId && c.UserId == userId);
    }
}
