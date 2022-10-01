
using CarsServer.Models;
using Microsoft.EntityFrameworkCore;

namespace CarsServer.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
            //Database.EnsureDeleted();   // удаляем бд со старой схемой
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           /* modelBuilder.Entity<Fridge_Product>()
            .HasOne(f => f.Fridge)
            .WithMany(fp => fp.Fridge_Products)
            .HasForeignKey(fi => fi.FridgeId);

            modelBuilder.Entity<Fridge_Product>()
            .HasOne(p => p.Product)
            .WithMany(fp => fp.Fridge_Products)
            .HasForeignKey(pi => pi.ProductId);

            modelBuilder.Entity<Fridge>()
            .HasOne(m => m.FridgeModel)
            .WithMany(fm => fm.Fridges)
            .HasForeignKey(fk => fk.FridgeModelId);*/
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Car_Color> Car_Colors { get; set; }
        public DbSet<Car_Prop_Value> Car_Prop_Values { get; set; }
        public DbSet<Color> Colors { get; set; }
        public DbSet<Company> Componies { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Property_PropertyValue> Property_PropertyValues { get; set; }
        public DbSet<PropValue> PropValues { get; set; }

    }
}
