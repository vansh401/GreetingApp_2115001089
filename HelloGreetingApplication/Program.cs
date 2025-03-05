using NLog;
using NLog.Web;
using NLog.Config;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
LogManager.Configuration = new XmlLoggingConfiguration("C:\\Users\\Lenovo\\source\\repos\\HelloGreetingApplication\\NLog.config");

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
