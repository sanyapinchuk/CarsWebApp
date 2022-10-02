using Microsoft.EntityFrameworkCore;
using Domain;

namespace Applicaton.Interfaces
{
    public interface IDataContext
    {
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

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
