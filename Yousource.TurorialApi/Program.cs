using Yousource.TutorialApi.Extensions.Injection;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var env = builder.Environment;

var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(env.ContentRootPath)
    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: false, reloadOnChange: true)
    .AddEnvironmentVariables();

var configuration = configurationBuilder.Build();

services.AddControllers();
services.AddApplicationInsightsTelemetry(configuration.GetSection("ApplicationInsights")["InstrumentationKey"]);

// Injection
services.ConfigureTutorialService(configuration);
services.InjectInfrastructure(configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

var allowedOrigins = configuration.GetSection("AllowedOrigins").Get<string[]>();
app.UseCors(cors => cors
    .WithOrigins(allowedOrigins)
    .AllowAnyMethod()
    .AllowAnyHeader());

if (env.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
