namespace Domain
{
    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = String.Empty;

        public virtual List<Car_Color>? Car_Colors { get; set; }
    }
}
