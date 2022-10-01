namespace CarsServer.Models
{
    public class Car_Color
    {
        public int Id { get; set; }

        public int CarId { get; set; }
        public virtual Car? Car { get; set; }
        public int ColorId { get; set; }
        public virtual Color? Color { get; set; }
    }
}
