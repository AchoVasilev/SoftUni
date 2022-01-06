using System.Collections.Generic;
using CarShop.ViewModels;

namespace CarShop.Services
{
    public interface ICarsService
    {
        string AddCar(string model, int year, string image, string plateNumber, string ownerId);

        ICollection<AllCarsViewModel> ListUserCars(string userId);

        ICollection<AllCarsViewModel> ListMechanicCars();
    }
}
