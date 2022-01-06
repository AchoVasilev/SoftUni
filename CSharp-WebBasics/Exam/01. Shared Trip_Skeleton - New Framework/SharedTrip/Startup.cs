using System.Threading.Tasks;

using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using SharedTrip.Data;
using Microsoft.EntityFrameworkCore;
using SharedTrip.Services.Contracts;
using SharedTrip.Services;
using CarShop.Services;

namespace SharedTrip
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
                    .Add<ITripsService, TripsService>()
                    .Add<ApplicationDbContext>())
                .WithConfiguration<ApplicationDbContext>(context => context
                    .Database.Migrate())
                .Start();
    }
}
