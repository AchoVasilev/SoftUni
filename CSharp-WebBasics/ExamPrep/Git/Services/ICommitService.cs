using Git.ViewModels.Commits;
using System.Collections.Generic;

namespace Git.Services
{
    public interface ICommitService
    {
        CommitToRepositoryFormModel Create(string repositoryId);

        string Create(CreateCommitFormModel model, string userId);

        IEnumerable<CommitListingFormModel> AllCommits(string userId);

        string Delete(string id);

        bool IsCreator(string userId, string commitId);
    }
}
