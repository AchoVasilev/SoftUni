using Git.Data;
using Git.ViewModels.Commits;
using System.Collections.Generic;
using System.Linq;

namespace Git.Services
{
    public class CommitService : ICommitService
    {
        private ApplicationDbContext data;

        public CommitService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<CommitListingFormModel> AllCommits(string userId) 
            => this.data.Commits
                .Where(x => x.CreatorId == userId)
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new CommitListingFormModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Repository = x.Repository.Name,
                    CreatedOn = x.CreatedOn.ToString("F")
                })
                .ToList();

        public CommitToRepositoryFormModel Create(string repositoryId)
            => this.data.Repositories
                   .Where(x => x.Id == repositoryId)
                   .Select(x => new CommitToRepositoryFormModel
                   {
                       Id = x.Id,
                       Name = x.Name
                   })
                   .FirstOrDefault();

        public string Create(CreateCommitFormModel model, string userId)
        {
            var commit = new Commit()
            {
                Description = model.Description,
                RepositoryId = model.Id,
                CreatorId = userId
            };

            this.data.Commits.Add(commit);
            this.data.SaveChanges();

            return commit.Id;
        }

        public string Delete(string id)
        {
            var commit = this.data.Commits.Find(id);

            this.data.Commits.Remove(commit);
            this.data.SaveChanges();

            return commit.Id;
        }

        public bool IsCreator(string userId, string commitId)
        {
            var commit = this.data.Commits
                .FirstOrDefault(x => x.Id == commitId && x.CreatorId == userId);

            if (commit == null)
            {
                return false;
            }

            return true;
        }
    }
}
