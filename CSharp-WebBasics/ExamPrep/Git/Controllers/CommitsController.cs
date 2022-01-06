using Git.Data;
using Git.Services;
using Git.ViewModels.Commits;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace Git.Controllers
{
    public class CommitsController : Controller
    {
        private readonly ICommitService commitService;
        private readonly ApplicationDbContext data;

        public CommitsController(ICommitService commitService, ApplicationDbContext data)
        {
            this.commitService = commitService;
            this.data = data;
        }

        [Authorize]
        public HttpResponse Create(string id)
        {
            var repository = this.commitService.Create(id);

            if (repository == null)
            {
                return BadRequest();
            }

            return this.View(repository);
        }

        [Authorize]
        [HttpPost]
        public HttpResponse Create(CreateCommitFormModel model)
        {
            if (!this.data.Repositories.Any(x => x.Id == model.Id))
            {
                return NotFound();
            }

            this.commitService.Create(model, this.User.Id);

            return Redirect("/Repositories/All");
        }

        [Authorize]
        public HttpResponse All() => View(this.commitService.AllCommits(this.User.Id));

        [Authorize]
        public HttpResponse Delete(string id)
        {
            if (this.commitService.IsCreator(this.User.Id, id))
            {
                return BadRequest();
            }

            this.commitService.Delete(id);

            return Redirect("/Commits/All");
        }
    }
}
