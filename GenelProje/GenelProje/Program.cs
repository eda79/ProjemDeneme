using GenelProje.Models;
using GenelProje.Service;
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connectionString = builder.Configuration.GetConnectionString("db");
var connectionString2 = builder.Configuration.GetConnectionString("NorthwindContext");


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NorthwndContext>(options => options.UseSqlServer(connectionString2));

builder.Services.AddHangfire(configuration =>
{
    configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UseSqlServerStorage(connectionString);
});

builder.Services.AddHangfireServer();

builder.Services.AddTransient<IServiceManagement, ServiceManagement>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHangfireDashboard("/hangfire", new DashboardOptions()
{
    DashboardTitle = "My Dashboard",
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter()
        {
            Pass = "deneme",
            User = "deneme"
        }
    }
});

app.MapControllers();

app.Run();
