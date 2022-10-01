using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.EntityTypeConfiguration
{
    public class CarsDbConfiguration : 
        IEntityTypeConfiguration<Car>, 
        IEntityTypeConfiguration<Car_Color>,
        IEntityTypeConfiguration<Car_Prop_Value>,
        IEntityTypeConfiguration<Color>,
        IEntityTypeConfiguration<Company>,
        IEntityTypeConfiguration<Model>,
        IEntityTypeConfiguration<Property>,
        IEntityTypeConfiguration<Property_PropertyValue>,
        IEntityTypeConfiguration<PropValue>
    {
        public void Configure(EntityTypeBuilder<Car> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c=>c.Id).IsUnique();
            
            ////
        }

        public void Configure(EntityTypeBuilder<Car_Color> builder)
        {
            builder.HasKey(cc => cc.Id);
            builder.HasIndex(cc=>cc.Id).IsUnique();

            builder.HasOne(cc=>cc.Car).WithMany(c=>c.Car_Colors).HasForeignKey(cc=>cc.CarId);
            builder.HasOne(cc => cc.Color).WithMany(c => c.Car_Colors).HasForeignKey(cc => cc.ColorId);
            
        }

        public void Configure(EntityTypeBuilder<Car_Prop_Value> builder)
        {
            builder.HasKey(cpv => cpv.Id);
            builder.HasIndex(cpv => cpv.Id).IsUnique();

            builder.HasOne(cpv => cpv.Car).WithMany(c=>c.Car_Prop_Values).HasForeignKey(cpv => cpv.CarId);
            builder.HasOne(cpv => cpv.Prop_Value).WithMany(c => c.Car_Prop_Values).HasForeignKey(cpv => cpv.Prop_ValueId);
        }

        public void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c=>c.Name).HasMaxLength(100);
        }

        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.Name).HasMaxLength(100);
        }

        public void Configure(EntityTypeBuilder<Model> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id).IsUnique();
            builder.Property(m => m.Name).HasMaxLength(100);

            builder.HasOne(m => m.Car).WithOne(c => c.Model).HasForeignKey<Car>(c => c.ModelId);
            builder.HasOne(m => m.Company).WithMany(c=>c.Models).HasForeignKey(c=>c.ComponyId);

        }

        public void Configure(EntityTypeBuilder<Property> builder)
        {
            builder.HasKey(c => c.Id);
            builder.HasIndex(c => c.Id).IsUnique();
            builder.Property(c => c.Name).HasMaxLength(100);
        }

        public void Configure(EntityTypeBuilder<Property_PropertyValue> builder)
        {
            builder.HasKey(m => m.Id);
            builder.HasIndex(m => m.Id).IsUnique();

            builder.HasOne(ppv => ppv.Property)
                .WithMany(p => p.Property_PropertyValues)
                .HasForeignKey(ppv => ppv.PropertyId);

            builder.HasOne(ppv=>ppv.PropValue)
                .WithMany(pv=>pv.Property_PropertyValues)
                .HasForeignKey(ppv=>ppv.PropValueId);
        }

        public void Configure(EntityTypeBuilder<PropValue> builder)
        {
            builder.HasKey(pv => pv.Id);
            builder.HasIndex(pv => pv.Id).IsUnique();
            builder.Property(pv => pv.Value).HasMaxLength(100);
        }
    }
}
