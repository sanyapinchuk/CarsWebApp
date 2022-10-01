
using Applicaton.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;
using Persistence.EntityTypeConfiguration;

namespace CarsServer.Data
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
            modelBuilder.ApplyConfiguration<Car_Prop_Value>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Color>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Company>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Model>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Property>(dbConfiguration);
            modelBuilder.ApplyConfiguration<Property_PropertyValue>(dbConfiguration);
            modelBuilder.ApplyConfiguration<PropValue>(dbConfiguration);

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Car_Color> Car_Colors { get; set; }
        public DbSet<Car_Prop_Value> Car_Prop_Values { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Property_PropertyValue> Property_PropertyValues { get; set; }
        public DbSet<PropValue> PropValues { get; set; }
    }
}
