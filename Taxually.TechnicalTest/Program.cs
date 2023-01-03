using NLog;
using NLog.Extensions.Logging;
using NLog.Targets;
using NLog.Web;
using Taxually.TechnicalTest;

var logger = LogManager.Setup().LoadConfiguration(c =>
{
    c.ForLogger("*").FilterMinLevel(NLog.LogLevel.Info).WriteTo(new ConsoleTarget("logconsole")).WithAsync();
    c.ForLogger().WriteTo(new FileTarget("logfile") { FileName = "file.txt" }).WithAsync();
}).GetCurrentClassLogger();

var builder = WebApplication.CreateBuilder(args);
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
startup.Configure(app, builder.Environment); // calling Configure method

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
var config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .Build();
LogManager.Configuration = new NLogLoggingConfiguration(config.GetSection("NLog"));
logger.Info($"Starting service: name=VATRegistration, currentDir=\"{Directory.GetCurrentDirectory()}\"");

app.Run();
