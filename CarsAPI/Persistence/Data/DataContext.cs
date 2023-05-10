
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
            modelBuilder.ApplyConfiguration<Car_Color>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Car__Property_PropValue>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Color>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Company>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Model>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Property>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Property_PropValue>(dbConfiguration);
            modelBuilder.ApplyConfiguration<PropValue>(dbConfiguration);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Car_Color> Car_Colors { get; set; }
        public DbSet<Car__Property_PropValue> Car__Property_PropValues { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Property_PropValue> Property_PropValues { get; set; }
        public DbSet<PropValue> PropValues { get; set; }
        public DbSet<Image> Images { get; set; }
    }
}
