using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Git.Data;
using System.Threading.Tasks;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using Git.Services;

namespace Git
{
    public class Startup
    {
        public static async Task Main()
            => await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services
                      .Add<IViewEngine, CompilationViewEngine>()
                      .Add<IValidator, Validator>()
                      .Add<IPasswordHasher, PasswordHasher>()
                      .Add<IUsersService, UsersService>()
                      .Add<IRepositoriesService, RepositoriesService>()
                      .Add<ICommitService, CommitService>()
                      .Add<ApplicationDbContext>())
                .WithConfiguration<ApplicationDbContext>(context => context
                       .Database.Migrate())
                .Start();
    }
}
