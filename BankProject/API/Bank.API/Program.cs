using Bank.API.Extensions;
using BANK.API.Extensions.Middleware;
using BANK.API.Models;
using BANK.BusinessLayer.Extensions;
using BANK.BusinessLayer.Mapping;
using BANK.DataLayer.Data;
using DataLayer.Data.Infrastructure;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Serilog;

var logger = NLog.LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddJwtAuthentication(configuration["Jwt:Key"]);
builder.Services.AddReactCors();

builder.Services.AddDataAccessLayer(configuration);

builder.Services.AddBusinessLogicLayer();
builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });
});

builder.Services.AddAuthorization();
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Host.ConfigureLogging((context, logging) =>
{
    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(context.Configuration)
        .CreateLogger();

    logging.ClearProviders();
    logging.AddSerilog(logger, dispose: true);
});

var app = builder.Build();

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<AutoEventLoggingMiddleware>();
app.UseMiddleware<ErrorLoggingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<BankDbContext>();
    DbInitializer.InitializeDataBase(context);
}

app.UseCors("AllowReactApp");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

app.UseStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();
