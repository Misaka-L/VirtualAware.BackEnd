using Refit;
using Serilog;
using Serilog.Events;
using VRCFlightRadar;
using VRCFlightRadar.Filters;
using VRCFlightRadar.Options;
using VRCFlightRadar.Services;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .WriteTo.Debug()
    .WriteTo.File("Logs/VRCFlightRadar-.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

builder.Services.AddSingleton<ExceptionFilter>();

builder.Services.AddControllers(options => {
    options.Filters.Add<ExceptionFilter>();
});

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Options
builder.Services.Configure<VRChatApiOption>(builder.Configuration.GetSection("VRChat"));

// VRChat Api Services
builder.Services.AddTransient<VRChatApiHandler>();
builder.Services.AddRefitClient<IVRChatApi>()
    .ConfigureHttpClient(client => client.BaseAddress = new Uri("https://api.vrchat.cloud/api/1"))
    .AddHttpMessageHandler<VRChatApiHandler>();

// FlightRadar
builder.Services.AddSingleton<FlightRadarService>();

var app = builder.Build();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment()) {
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
