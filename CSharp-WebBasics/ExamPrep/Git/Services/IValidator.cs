using Git.ViewModels.Commits;
using Git.ViewModels.Repositories;
using Git.ViewModels.Users;
using System.Collections.Generic;

namespace Git.Services
{
    public interface IValidator
    {
        ICollection<string> ValidateUser(RegisterUserFormModel model);

        ICollection<string> ValidateRepository(AddRepositoryFormModel model);

        ICollection<string> ValidateCommit(CreateCommitFormModel model);
    }
}
