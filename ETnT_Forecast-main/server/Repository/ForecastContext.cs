using DataAccess.DbSets;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class ForecastContext : DbContext
    {
        public ForecastContext()
        {
        }

        public ForecastContext(DbContextOptions<ForecastContext> options) : base(options)
        {
        }

        public virtual DbSet<Forecast> Forecasts { get; set; }
        public virtual DbSet<Business> Businesses { get; set; }
        public virtual DbSet<Capability> Capabilities { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<ForecastData> ForecastData { get; set; }
        public virtual DbSet<Org> Orgs { get; set; }
        public virtual DbSet<Project> Projects { get; set; }
        public virtual DbSet<Skill> Skills { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<EventData> Task { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Forecast>()
                .HasMany(x => x.ForecastData)
                .WithOne(f => f.Forecast)
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            if (!options.IsConfigured)
                options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=#Iamdevil1;Database=etnt;");
        }
    }
}