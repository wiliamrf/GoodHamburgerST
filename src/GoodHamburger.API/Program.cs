using GoodHamburger.API.Configuration;
using GoodHamburger.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
{
    builder.Services.AddDbContext<GHContext>(options =>
        options.UseInMemoryDatabase(builder.Configuration.GetConnectionString("InMemoryConnection")));
}
else
{
    builder.Services.AddDbContext<GHContext>(options =>
                          options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
}



builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen();


builder.Services.AddDependeciesInjection();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseApiConfig(app.Environment);
if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == Environments.Development)
{
    app.SeedServiceConfig(app.Environment);
}
app.Run();
