using CodeMaze.WebAPI.Extensions;
using Contracts;
using LoggerService;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc.Formatters;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile("nlog.config");
var configuration = builder.Configuration;
// Add services to the container.
builder.Services.AddControllers(config => {
                                config.RespectBrowserAcceptHeader = true;
                                config.OutputFormatters.Add(new XmlSerializerOutputFormatter());
                                config.ReturnHttpNotAcceptable = true;
                                }).AddCustomCSVFormatter()
    .AddApplicationPart(typeof(Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureSqlContext(configuration);
builder.Services.AddAutoMapper(typeof(Program));
var app = builder.Build();
var logger = app.Services.GetRequiredService<ILoggerManager>();
// Configure the HTTP request pipeline.

app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

app.UseHsts(); 

app.UseAuthorization();

app.MapControllers();

app.Run();
