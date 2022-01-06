using CarShop.ViewModels;

namespace CarShop.Services
{
    public interface IIssuesService
    {
        IssuesAllViewModel GetAllIssues(string carId);

        void AddIssue(string carId, string description);

        void Delete(string issueId, string carId);

        void Fix(string issueId);
    }
}
