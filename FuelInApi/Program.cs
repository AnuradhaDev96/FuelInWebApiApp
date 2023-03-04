using FuelInApi.Data;
using FuelInApi.Interfaces;
using FuelInApi.Models.Dtos;
using FuelInApi.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add cors
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(x =>
    {
        x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        var enumConverter = new JsonStringEnumConverter();
        x.JsonSerializerOptions.Converters.Add(enumConverter);
    });

// Add mapping profile
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.Configure<MailSettingsDto>(builder.Configuration.GetSection("MailSettings"));

// Add dependency injections
builder.Services.AddScoped<IUserManagementRepository, UserManagementRepository>();
builder.Services.AddScoped<IFuelStationManagementInterface, FuelStationManagementRepository>();
builder.Services.AddScoped<IMailService, MailService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add DbContext
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(builder =>
{
    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
});

app.MapControllers();

app.Run();
