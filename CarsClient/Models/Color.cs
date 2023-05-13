namespace CarsClient.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public virtual List<Car_Color>? Car_Colors { get; set; }
    }
}
