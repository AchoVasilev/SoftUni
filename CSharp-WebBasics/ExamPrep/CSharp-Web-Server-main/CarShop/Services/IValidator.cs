using System.Collections.Generic;
using CarShop.ViewModels;

namespace CarShop.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserViewModel model);

        ICollection<string> ValidateCar(AddCarViewModel model);

        ICollection<string> ValidateIssue(AddIssueFormModel model);
    }
}
