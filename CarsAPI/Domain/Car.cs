namespace Domain
{
    public class Car
    {
        public Guid Id { get; set; }        
        public int Price { get; set; }
        public int ProductionYear { get; set; }

        public Guid ModelId { get; set; }
        public virtual Model? Model { get; set; }

        public Guid ColorId { get; set; }
        public virtual Color? Color { get; set; }


        public virtual List<Car_PropValue>? Car_PropValues { get; set; }
        public virtual List<Car_Image>? Car_Images { get; set; }

    }
}
 