using System;
using System.Collections.Generic;
using System.Linq;
using Git.Data;
using Git.ViewModels.Repositories;

namespace Git.Services
{
    public class RepositoriesService : IRepositoriesService
    {
        private readonly ApplicationDbContext data;

        public RepositoriesService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public IEnumerable<RepositoryAllFormModel> GetAllPublicRepositories()
        {
            var repositories = this.data.Repositories
                .Where(x => x.IsPublic)
                .Select(x => new RepositoryAllFormModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Owner = x.Owner.Username,
                    CreatedOn = x.CreatedOn.ToString(),
                    Commits = x.Commits.Count
                })
                .ToList();

            return repositories;
        }

        public string Create(string name, string type, string userId)
        {
            var repository = new Repository()
            {
                Name = name,
                IsPublic = type == DataConstants.PublicRepositoryType,
                OwnerId = userId,
                CreatedOn = DateTime.UtcNow,
            };

            this.data.Repositories.Add(repository);
            this.data.SaveChanges();

            return repository.Id;
        }

        public RepositoryAllFormModel GetRepositoryId(string repositoryId)
            => this.data.Repositories
                   .Where(x => x.Id == repositoryId)
                   .Select(x => new RepositoryAllFormModel
                   {
                       Id = x.Id,
                       Name = x.Name,
                       CreatedOn = x.CreatedOn.ToString(),
                       Commits = x.Commits.Count,
                       Owner = x.Owner.Username
                   })
                    .FirstOrDefault();
    }
}
