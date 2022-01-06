using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Git.Services
{
    using static Data.DataConstants;

    public class Validator : IValidator
    {
        public ICollection<string> ValidateCommit(CreateCommitFormModel model)
        {
            var errors = new List<string>();

            if (model.Description.Length < CommitDescriptionMinLength)
            {
                errors.Add($"The minimum description length is {CommitDescriptionMinLength}");
            }

            return errors;
        }

        public ICollection<string> ValidateRepository(AddRepositoryFormModel model)
        {
            var errors = new List<string>();

            if (model.Name.Length > RepositoryMaxName || model.Name.Length < RepositoryMinName)
            {
                errors.Add($"Repository '{model.Name}' is not valid. It must be between {RepositoryMinName} and {RepositoryMaxName} characters long.");
            }

            if (model.RepositoryType != PrivateRepositoryType && model.RepositoryType != PublicRepositoryType)
            {
                errors.Add($"Repository should be either '{PrivateRepositoryType}' or '{PublicRepositoryType}'.");
            }

            return errors;
        }

        public ICollection<string> ValidateUser(RegisterUserFormModel model)
        {
            var errors = new List<string>();

            if (model.Username.Length > DefaultMaxLength || model.Username.Length < UserMinUsername)
            {
                errors.Add($"Username '{model.Username}' is not valid. It must be between {UserMinUsername} and {DefaultMaxLength} characters long.");
            }

            if (!Regex.IsMatch(model.Email, UserEmailRegularExpression))
            {
                errors.Add($"Email {model.Email} is not a valid e-mail address.");
            }

            if (model.Password.Length > DefaultMaxLength || model.Password.Length < UserMinPassword)
            {
                errors.Add($"The provided password is not valid. It must be between {UserMinPassword} and {DefaultMaxLength} characters long.");
            }

            if (model.Password != model.ConfirmPassword)
            {
                errors.Add("The provided password is different from the confirmation password");
            }

            return errors;
        }
    }
}
