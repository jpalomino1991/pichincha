using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Instrumentation.AspNetCore;
using Movimiento.WebAPI.App.GlobalConfig;
using Movimiento.WebAPI.App.Middleware;
using AutoMapper;
using Movimiento.WebAPI.App.DependencyInjection;
using Movimiento.Application.Common.Mapping;
using Movimiento.Application.Common.Services;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;
// Register Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder =>
    builder.RegisterModule(new MediatorModule()));
// Add services to the container.

builder.Services.AddRepositories();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Custom dependencies
builder.Services.AddGlobalSettings();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddHealthChecks();
// AutoMapper
var config = new MapperConfiguration(cfg =>
{
    cfg.AddProfile(new MovementMaps());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

// HttpClient

builder.Services.AddHttpClient<IClientService, ClientService>(client =>
{
   client.BaseAddress = new Uri(configuration.GetValue<string>("Endpoint:UrlClient"));
   client.Timeout = TimeSpan.FromSeconds(20);
});
builder.Services.AddHttpClient<IAccountService, AccountService>(client =>
{
   client.BaseAddress = new Uri(configuration.GetValue<string>("Endpoint:UrlAccount"));
   client.Timeout = TimeSpan.FromSeconds(40);
});

builder.Services.Configure<AspNetCoreInstrumentationOptions>(builder.Configuration.GetSection("AspNetCoreInstrumentation"));
builder.Services.ConfigureSwagger();


var app = builder.Build();

app.UseSwaggerApi();

app.Logger.LogInformation("Use Health Checks");
app.UseHealthChecks("/Health", new HealthCheckOptions()
{
    ResultStatusCodes =
            {
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status200OK,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Degraded] = StatusCodes.Status503ServiceUnavailable,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
            }
});

app.UseMiddleware(typeof(ExceptionHandlingMiddleware));

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.Run();
