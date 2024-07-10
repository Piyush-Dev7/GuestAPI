using DataAccess.Core.Auth.Contract;
using DataAccess.Core.Repositories;
using DataAccess.Core.Repositories.Contract;
using GuestsAPI.Middlerwares;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("data-access-appsettings.json", optional: false, reloadOnChange: true).Build();
builder.Services.AddControllers();

//Add Key Vault Config stored separately. 
//Some additional configuration may require therefore add it this way instead of adding Json File directly in builder.Configuration.
builder.Configuration.AddConfiguration(config);

//Add Other Services
builder.Services.AddSingleton<ApplicationDbContext>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IGuestRepository, GuestRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Serilog
// Configure logging - make sure DB instance is created & connection string is working
//else comment Serilog region code and try again.
//once db instance is created and available uncomment this section.
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();
builder.Logging.AddSerilog(logger);
Serilog.Debugging.SelfLog.Enable(msg => Console.WriteLine(msg));
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
// Build service provider
// Perform database migration
using (var scope = app.Services.CreateScope())
{
    var dataContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dataContext.Database.Migrate();
}
// custom jwt auth middleware
app.UseMiddleware<JwtMiddleware>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
