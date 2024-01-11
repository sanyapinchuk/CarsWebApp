using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration
{
    public class CarsDbConfiguration : 
        IEntityTypeConfiguration<Car>, 
        IEntityTypeConfiguration<Car_Image>,
        IEntityTypeConfiguration<Car_PropValue>,
        IEntityTypeConfiguration<Image>,
        IEntityTypeConfiguration<Model>,
        IEntityTypeConfiguration<Property>,
        IEntityTypeConfiguration<CarType>,
        IEntityTypeConfiguration<PropValue>,
        IEntityTypeConfiguration<PropCategory>,
        IEntityTypeConfiguration<News>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c=>c.Id).IsUnique();

            builder.HasOne(c => c.Model)
                .WithMany(m => m.Cars)
                .HasForeignKey(c => c.ModelId);
        }

        public void Configure(EntityTypeBuilder<Car_PropValue> builder)
        {
            builder.HasKey(cpv => cpv.Id);
            builder.HasIndex(cpv => cpv.Id).IsUnique();

            builder.HasOne(cpv => cpv.Car)
                .WithMany(c=> c.Car_PropValues)
                .HasForeignKey(cpv => cpv.CarId);

            builder.HasOne(cpv => cpv.PropValue)
                .WithMany(pv=>pv.Car_PropValues)
                .HasForeignKey(cpv => cpv.PropValueId);
        }

        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id).IsUnique();
            builder.Property(m => m.Name).HasMaxLength(100);

            builder.HasOne(m => m.CarType).WithMany(ct => ct.Models)
                .HasForeignKey(m => m.CarTypeId);
        }

        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.IsKeyProperty).HasDefaultValue(false);
            builder.Property(c => c.Name).HasMaxLength(100);
        }

        public void Configure(EntityTypeBuilder<PropValue> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id).IsUnique();

            builder.HasOne(ppv => ppv.Property)
                .WithMany(p => p.PropValues)
                .HasForeignKey(ppv => ppv.PropertyId);

            builder.Property(pv => pv.Value).HasMaxLength(100);
        }

        public void Configure(EntityTypeBuilder<Car_Image> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id).IsUnique();

            builder.HasOne(ci => ci.Car)
                .WithMany(c => c.Car_Images)
                .HasForeignKey(ci => ci.CarId);

            builder.HasOne(ci => ci.Image)
                .WithMany(i => i.Car_Images)
                .HasForeignKey(ci => ci.ImageId);

            builder.Property(ci=>ci.IsMainImage).HasDefaultValue(false);
        }

        public void Configure(EntityTypeBuilder<Image> builder)
        {
            builder.HasKey(i => i.Id);
            builder.HasIndex(i => i.Id).IsUnique();
        }

        public void Configure(EntityTypeBuilder<CarType> builder)
        {
            builder.HasKey(ct => ct.Id);
            builder.HasIndex(ct => ct.Id).IsUnique();
            builder.Property(ct => ct.Name).HasMaxLength(100);
        }

        public void Configure(EntityTypeBuilder<PropCategory> builder)
        {
            builder.HasKey(ct => ct.Id);
            builder.HasIndex(ct => ct.Id).IsUnique();
        }

        public void Configure(EntityTypeBuilder<News> builder)
        {
            builder.HasKey(ct => ct.Id);
            builder.HasIndex(ct => ct.Id).IsUnique();
        }
    }
}
