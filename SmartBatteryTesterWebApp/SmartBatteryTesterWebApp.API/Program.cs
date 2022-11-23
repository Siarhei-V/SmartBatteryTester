using SmartBatteryTesterWebApp.BLL.Interfaces;
using SmartBatteryTesterWebApp.BLL.Services;
using SmartBatteryTesterWebApp.DAL.EF;
using SmartBatteryTesterWebApp.DAL.Inrerfaces;
using SmartBatteryTesterWebApp.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<IMeasurementRepository, EFMeasurementRepository>();
builder.Services.AddScoped<IMeasurementSetRepository, EFMeasurementSetRepository>();
builder.Services.AddScoped<IMeasurementInputService, MeasurementInputService>();
builder.Services.AddScoped<IMeasurementOutputService, MeasurementOutputService>();
builder.Services.AddScoped<ApplicationContext>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
