using Git.Services;
using Git.ViewModels.Repositories;
using MyWebServer.Controllers;
using MyWebServer.Http;
using System.Linq;

namespace Git.Controllers
{
    public class RepositoriesController : Controller
    {
        private readonly IRepositoriesService repositoriesService;
        private readonly IValidator validator;

        public RepositoriesController(IRepositoriesService repositoriesService, IValidator validator)
        {
            this.repositoriesService = repositoriesService;
            this.validator = validator;
        }

        [Authorize]
        public HttpResponse All()
        {
            var repositories = this.repositoriesService.GetAllPublicRepositories();

            return this.View(repositories);
        }

        [Authorize]
        public HttpResponse Create() => this.View();

        [HttpPost]
        [Authorize]
        public HttpResponse Create(AddRepositoryFormModel model)
        {
            var modelErrors = this.validator.ValidateRepository(model);

            if (modelErrors.Any())
            {
                return Error(modelErrors);
            }

            this.repositoriesService.Create(model.Name, model.RepositoryType, this.User.Id);

            return Redirect("/Repositories/All");
        }
    }
}
