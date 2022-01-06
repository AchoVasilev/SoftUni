using System.Collections.Generic;

namespace CarShop.ViewModels
{
    public class IssuesAllViewModel
    {
        public string CarId { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public bool UserIsMechanic { get; set; }

        public IEnumerable<IssuesViewModel> Issues { get; set; }
    }
}
