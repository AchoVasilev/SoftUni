namespace SharedTrip.ViewModels.Trips
{
    public class TripDetailsViewModel
    {
        public string Id { get; init; }

        public string StartPoint { get; init; }

        public string EndPoint { get; init; }

        public string DepartureTime { get; init; }

        public int Seats { get; init; }

        public string Image { get; init; }

        public string Description { get; init; }
    }
}
