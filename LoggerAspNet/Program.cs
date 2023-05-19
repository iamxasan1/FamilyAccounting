using LoggerAspNet.Middlewares;
using LoggerAspNet.MyLogger;
using LoggerAspNet.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<Logger>();
builder.Logging.AddProvider(new MyLoggerProvider());
var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<ErrorHandleMiddleware>();

app.MapControllers();
app.MapGet("Xasan", () => "xexesalom");

app.Run();
