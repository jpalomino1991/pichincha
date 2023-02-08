using Autofac.Extensions.DependencyInjection;
using Autofac;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Instrumentation.AspNetCore;
using Cliente.WebAPI.App.GlobalConfig;
using Cliente.WebAPI.App.Middleware;
using AutoMapper;
using Cliente.WebAPI.App.DependencyInjection;
using Cliente.Application.Common.Mapping;

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
    cfg.AddProfile(new ClientMaps());
});
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);

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
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy] = StatusCodes.Status201Created,
                [Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Unhealthy] = StatusCodes.Status404NotFound,
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
