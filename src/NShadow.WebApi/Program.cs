using NShadow.WebApi.Dependencies;

var builder = WebApplication.CreateEmptyBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory()
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Configuration.AddJsonFile("serilog.json", optional: false);
builder.Configuration.AddJsonFile("config.json", optional: false);
builder.Configuration.AddUserSecrets(typeof(Program).Assembly);
builder.WebHost.UseKestrelCore();
builder.WebHost.UseKestrelHttpsConfiguration();

builder.Services.ConfigureServices(builder.Environment, builder.Configuration);

var app = builder.Build();

app.UseHealthChecks("/health");
app.MapHealthChecks("/health");

app.Run();
