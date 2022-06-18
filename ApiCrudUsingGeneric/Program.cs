using ApiCrudUsingGeneric.IService;
using ApiCrudUsingGeneric.Models;
using ApiCrudUsingGeneric.Service;
using Logging.Extensions;
using Logging.NLogCustom;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
#region UserNLog
Console.WriteLine($"Welcome to SV.HRM------------->{DateTime.Now}");

var configFilePath = configuration.GetSection("Logging:Providers:NLog:ConfigFilePath").Get<string>();
LoggingBuilderExten.UseNLog(configFilePath, configuration.GetSection("Logging:KafkaTaget").Get<string>(), LogSourceTypeEnums.HRM_Service_QuanTriDanhMuc);

#endregion UserNLog

// Add services to the container.

#region Add Logger service
builder.Services.AddLogging();
#endregion

builder.Services.AddControllers();
builder.Services.AddScoped<IGenericService<Student>, StudentService>();
builder.Services.AddScoped<IGenericService<Teacher>, TeacherService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
