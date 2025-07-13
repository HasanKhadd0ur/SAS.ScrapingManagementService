using SAS.ScrapingManagementService.API.DependencyInjection;
using SAS.ScrapingManagementService.Application.ApplicationDependencyInjection;
using SAS.ScrapingManagementService.Presentation.DependencyInjection;
using SAS.ScrapingManagementService.Infrastructure.Services.DependencyInjection;
using SAS.ScrapingManagementService.Infrastructure.Persistence.DependencyInjection;


var builder = WebApplication.CreateBuilder(args);

// Access configuration
var configuration = builder.Configuration;


// Add services to the container.

// adding dependency injection 
builder.Services
    .AddAPI(configuration)
    .AddApplication()
    .AddPresentation()
    .AddInfrastructureSevices(configuration)
    .AddPersistence(configuration);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); 
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAS.Api v1");
    });
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontendDev");

app.UseAuthorization();

app.MapControllers();

app.Run();
