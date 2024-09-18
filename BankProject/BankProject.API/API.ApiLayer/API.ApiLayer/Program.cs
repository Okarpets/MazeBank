using API.BusinessLogicLayer.Extensions;
using API.DataLayer.Data.Infrastructure;
using DataLayer.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddDataAccessLayer(builder.Configuration);
// Injection ^^^ DAL

builder.Services.AddBusinessLogicLayer();
// Injection ^^^ BLL

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMyOrigin",
        builder => builder
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());
});

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<MazeBankDbContext>();
        DbInitializer.InitializeDataBase(context);
    }
    catch (Exception)
    {
        throw;
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors("AllowMyOrigin");

app.Run();