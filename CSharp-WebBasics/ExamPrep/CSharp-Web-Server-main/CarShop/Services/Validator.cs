using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using CarShop.ViewModels;
using static CarShop.Data.DataConstants;

namespace CarShop.Services
{
    public class Validator : IValidator
    {
        public ICollection<string> ValidateCar(AddCarViewModel model)
        {
            var errors = new List<string>();

            if (model.Model.Length > CarMaxLength || model.Model.Length < CarMinLength)
            {
                errors.Add($"Username should be between {CarMinLength} and {CarMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.PlateNumber, CarPlateNumberRegularExpression))
            {
                errors.Add("The plate number should be in this format: AA0000AA");
            }

            if (!Uri.IsWellFormedUriString(model.Image, UriKind.Absolute))
            {
                errors.Add($"Image {model.Image} is not a valid URL.");
            }

            return errors;
        }

        public ICollection<string> ValidateIssue(AddIssueFormModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < DescriptionMinLength)
            {
                errors.Add($"Description should be atleast {DescriptionMinLength} characters long.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserViewModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length > UsernameMaxLength || model.Username.Length < UsernameMinLength)
            {
                errors.Add($"Username should be between {UsernameMinLength} and {UsernameMaxLength} characters long.");
            }

            if (model.Password.Length < PasswordMinLength)
            {
                errors.Add($"Password should be atleast {PasswordMinLength} characters long.");
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

            if (model.UserType != "Mechanic" && model.UserType != "Client")
            {
                errors.Add("Invalid user type.");
            }

            return errors;
        }
    }
}
