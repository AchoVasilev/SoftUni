using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace CarShop.Services
{
    public class CarsService : ICarsService
    {
        private readonly ApplicationDbContext data;

        public CarsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public string AddCar(string model, int year, string image, string plateNumber, string ownerId)
        {
            var car = new Car()
            {
                Model = model,
                Year = year,
                PictureUrl = image,
                PlateNumber = plateNumber,
                OwnerId = ownerId
            };

            this.data.Cars.Add(car);
            this.data.SaveChanges();

            return car.Id;
        }

        public ICollection<AllCarsViewModel> ListUserCars(string userId)
        {
            var cars = this.data.Cars
                .Where(x => x.OwnerId == userId)
                .Select(x => new AllCarsViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    Year = x.Year,
                    Image = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    RemainingIssues = x.issues.Where(x => x.IsFixed == false).Count(),
                    FixedIssues = x.issues.Where(x => x.IsFixed).Count()
                })
                .ToList();

            return cars;
        }

        public ICollection<AllCarsViewModel> ListMechanicCars()
        {
            var cars = this.data.Cars
                .Where(x => x.issues.Any(i => i.IsFixed == false))
                .Select(x => new AllCarsViewModel
                {
                    Id = x.Id,
                    Model = x.Model,
                    Year = x.Year,
                    Image = x.PictureUrl,
                    PlateNumber = x.PlateNumber,
                    RemainingIssues = x.issues.Where(x => x.IsFixed == false).Count(),
                    FixedIssues = x.issues.Where(x => x.IsFixed).Count()
                })
                .ToList();

            return cars;
        }
    }
}
