using DataAccess.Core.Converters;
using DataAccess.Core.DataModel;
using DataAccess.Core.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class ApplicationDbContext : DbContext
{
    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<GuestContact> GuestContacts { get; set; }
    public DbSet<UserGuestsMapping> UserGuests { get; set; }

    protected override void ConfigureConventions(ModelConfigurationBuilder builder)
    {
        builder.Properties<DateOnly>()
                .HaveConversion<DateOnlyConverter>()
                .HaveColumnType("date");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlServer(new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("data-access-appsettings.json", optional: false, reloadOnChange: true).Build().GetConnectionString("GuestDB"));
}
