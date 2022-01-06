using SharedTrip.ViewModels.Trips;
using System;
using System.Collections.Generic;

namespace SharedTrip.Services.Contracts
{
    public interface ITripsService
    {
        IEnumerable<TripListingViewModel> GetAllTrips();

        void CreateTrip(string startPoint, string endPoint, string departureTime, string imagePath, int seats, string description);

        TripDetailsViewModel GetTripDetails(string tripId);

        string GetTripId(string tripId);

        bool CheckForUserTrip(string tripId, string userId);

        void AddUserToTrip(string tripId, string userId);
    }
}
