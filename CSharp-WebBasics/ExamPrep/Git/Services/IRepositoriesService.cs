using System.Collections.Generic;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public interface IRepositoriesService
    {
        IEnumerable<RepositoryAllFormModel> GetAllPublicRepositories();

        string Create(string name, string type, string userId);

        RepositoryAllFormModel GetRepositoryId(string repositoryId);
    }
}
