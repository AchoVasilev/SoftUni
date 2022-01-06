using System.Collections.Generic;
using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;

namespace SharedTrip.Services.Contracts
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserViewModel model);

        ICollection<string> ValidateTrip(TripsAddViewModel model);
    }
}
