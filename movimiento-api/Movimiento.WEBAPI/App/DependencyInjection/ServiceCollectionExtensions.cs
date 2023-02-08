using Movimiento.Domain.Interfaces;
using Movimiento.Infraestructure.Data;
using Movimiento.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Movimiento.WebAPI.App.DependencyInjection
{
   public static class ServiceCollectionExtensions
   {
      public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<MovimientoContext>(options => options
                  .EnableSensitiveDataLogging()
                  .UseNpgsql(configuration.GetConnectionString("ConnectionString"), x => x.MigrationsAssembly(typeof(MovimientoContext).Assembly.FullName)));
      }

      public static void AddRepositories(this IServiceCollection services)
      {
         services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
            .AddScoped<IMovementRepository, MovementRepository>();
      }
   }
}