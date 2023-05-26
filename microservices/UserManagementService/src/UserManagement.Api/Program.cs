using FluentValidation.AspNetCore;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Reflection;
using UserManagement.Application;
using UserManagement.Infrastructure;
using UserManagement.Infrastructure.Persistence;
using Work.Rabbi.Common.Api.Filters;
using Work.Rabbi.Common.Api.Services;
using Work.Rabbi.Common.Interfaces;

var builder = WebApplication.CreateBuilder(args);

var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
builder.Configuration.SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{environment}.json", optional: true)
    .AddEnvironmentVariables();

// Add Logger
Log.Logger = new LoggerConfiguration()
    .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(builder.Configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    })
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Add services to the container.
builder.Services.AddControllers(options =>
        options.Filters.Add<ApiExceptionFilterAttribute>()
    )
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    })
    .AddFluentValidation(flv =>
    {
        flv.DisableDataAnnotationsValidation = true;
        flv.AutomaticValidationEnabled = false;
    });

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

builder.Services
    .AddApplication()
    .AddInfrastructure(builder.Configuration);
builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<ILoggedInUserService, LoggedinUserService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOpenTelemetryTracing(
    (builderTelemetry) => builderTelemetry
        .AddAspNetCoreInstrumentation()
        .AddHttpClientInstrumentation()
        .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("UserManagement.API"))
        .AddJaegerExporter(j =>
        {
            j.AgentHost = builder.Configuration["Jaeger:AgentHost"];
            j.AgentPort = Convert.ToInt32(builder.Configuration["Jaeger:AgentPort"]);
        })
    );

// Sql server health check
builder.Services.AddHealthChecks()
    .AddDbContextCheck<ApplicationDbContext>("configuratiodbcontext");

var app = builder.Build();

// Migrate database
//await app.MigrateDatabaseAsync<ApplicationDbContext>(async (context, services) =>
//{
//    await ConfigurationDbContextSeed.SeedAsync(context);
//});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Cofigure for health check
app.MapHealthChecks("/hc", new HealthCheckOptions()
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse,
});

//a basic health probe configuration that reports the app's availability to process requests (liveness) is sufficient to discover the status of the app.
app.MapHealthChecks("/liveness", new HealthCheckOptions()
{
    Predicate = r => r.Name.Contains("self"),
});

await app.RunAsync();
