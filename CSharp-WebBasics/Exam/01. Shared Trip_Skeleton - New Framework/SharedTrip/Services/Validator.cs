using SharedTrip.Services.Contracts;
using SharedTrip.ViewModels.Trips;
using SharedTrip.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static SharedTrip.Data.DataConstants;

namespace SharedTrip.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateTrip(TripsAddViewModel model)
        {
            var errors = new List<string>();

            if (model.Seats > SeatsMaxValue || model.Seats < SeatsMinValue)
            {
                errors.Add($"Seats should be between {SeatsMinValue} and {SeatsMaxValue}.");
            }

            if (!Uri.IsWellFormedUriString(model.ImagePath, UriKind.Absolute))
            {
                errors.Add($"Image {model.ImagePath} is not a valid URL.");
            }

            if (model.Description.Length > DescriptionMaxLength)
            {
                errors.Add($"Description should be less than {DescriptionMaxLength} characters long.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length > UserMaxUsernameLength || model.Username.Length < UserMinUsername)
            {
                errors.Add($"Username should be between {UserMinUsername} and {UserMaxUsernameLength} characters long.");
            }

            if (model.Password.Length < UserMinPassword)
            {
                errors.Add($"Password should be atleast {UserMinPassword} characters long.");
            }

            if (model.Password.Contains(' '))
            {
                errors.Add("Password cannot contain whitespaces.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("The password and its confirmation do not match.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add("Please enter a valid email.");
            }

            return errors;
        }
    }
}
