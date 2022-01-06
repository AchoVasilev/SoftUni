
namespace CarShop.ViewModels
{
    public class AllCarsViewModel
    {
        public string Id { get; init; }

        public string Model { get; init; }

        public int Year { get; init; }

        public string Image { get; init; }

        public string PlateNumber { get; set; }

        public int RemainingIssues { get; set; }

        public int FixedIssues { get; set; }
    }
}
