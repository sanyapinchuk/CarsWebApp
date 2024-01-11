using Microsoft.EntityFrameworkCore;
using Domain;

namespace Applicaton.Interfaces
{
    public interface IDataContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Car_Image> Car_Images { get; set; }
        public DbSet<Model> Models { get; set; }
        public DbSet<Property> Properties { get; set; }
        public DbSet<Car_PropValue> Car_PropValues { get; set; }
        public DbSet<PropValue> PropValues { get; set; }
        public DbSet<Image> Images { get; set; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
