using Cuenta.Domain.Interfaces;
using Cuenta.Infraestructure.Data;
using Cuenta.Infraestructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Account.WebAPI.App.DependencyInjection
{
   public static class ServiceCollectionExtensions
   {
      public static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
      {
         services.AddDbContext<AccountContext>(options => options
                  .EnableSensitiveDataLogging()
                  .UseNpgsql(configuration.GetConnectionString("ConnectionString"), x => x.MigrationsAssembly(typeof(AccountContext).Assembly.FullName)));
      }

      public static void AddRepositories(this IServiceCollection services)
      {
         services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>))
            .AddScoped<IAccountRepository, AccountRepository>();
      }
   }
}