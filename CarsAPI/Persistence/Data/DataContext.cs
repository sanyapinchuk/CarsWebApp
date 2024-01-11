
using Applicaton.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;

namespace Persistence.Data
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var dbConfiguration = new CarsDbConfiguration();

            modelBuilder.ApplyConfiguration<Car>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Car_Image>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Car_PropValue>(dbConfiguration);
            modelBuilder.ApplyConfiguration<CarType>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Model>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Property>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Image>(dbConfiguration);
            modelBuilder.ApplyConfiguration<PropValue>(dbConfiguration);
            modelBuilder.ApplyConfiguration<PropCategory>(dbConfiguration);
            modelBuilder.ApplyConfiguration<News>(dbConfiguration);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Car_Image> Car_Images { get; set; }
        public DbSet<Car_PropValue> Car_PropValues { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<PropValue> PropValues { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<PropCategory> PropCategories { get; set; }
    }
}
