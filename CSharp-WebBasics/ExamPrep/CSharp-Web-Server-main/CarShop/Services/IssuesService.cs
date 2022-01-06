using CarShop.Data;
using CarShop.Data.Models;
using CarShop.ViewModels;
using System.Linq;

namespace CarShop.Services
{
    public class IssuesService : IIssuesService
    {
        private readonly ApplicationDbContext data;

        public IssuesService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public void AddIssue(string carId, string description)
        {
            var issue = new Issue()
            {
                Description = description,
                CarId = carId,
                IsFixed = false
            };

            this.data.Add(issue);
            this.data.SaveChanges();
        }

        public void Delete(string issueId, string carId)
        {
            var issue = this.data.Issues
                .Where(x => x.Id == issueId && x.CarId == carId)
                .FirstOrDefault();

            this.data.Issues.Remove(issue);
            this.data.SaveChanges();
        }

        public void Fix(string issueId)
        {
            var issue = this.data.Issues.Find(issueId);
            issue.IsFixed = true;

            this.data.SaveChanges();
        }

        public IssuesAllViewModel GetAllIssues(string carId)
        {
            var car = this.data.Cars
                .Where(x => x.Id == carId)
                .Select(x => new IssuesAllViewModel
                {
                    Model = x.Model,
                    Year = x.Year,
                    CarId = carId,
                    Issues = this.data.Issues
                                   .Where(x => x.CarId == carId)
                                   .Select(i => new IssuesViewModel
                                   {
                                       Id = i.Id,
                                       Description = i.Description,
                                       IsFixed = i.IsFixed,
                                       IsFixedInformation = i.IsFixed ? "Yes" : "Not yet"
                                   })
                                   .ToList()
                })
                .FirstOrDefault();

            return car;
        }
    }
}
