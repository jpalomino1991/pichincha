using Cliente.Domain.Interfaces;
using Cliente.Infraestructure.Data;
using Cliente.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Cliente.WebAPI.App.DependencyInjection
{
   public static class ServiceCollectionExtensions
   {
      public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<ClientContext>(options => options
                  .EnableSensitiveDataLogging()
                  .UseNpgsql(configuration.GetConnectionString("ConnectionString"), x => x.MigrationsAssembly(typeof(ClientContext).Assembly.FullName)));
      }

      public static void AddRepositories(this IServiceCollection services)
      {
         services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
            .AddScoped<IPersonRepository, PersonRepository>()
            .AddScoped<IClientRepository, ClientRepository>();
      }
   }
}