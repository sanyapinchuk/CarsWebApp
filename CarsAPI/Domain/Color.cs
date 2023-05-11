namespace Domain
{
    public class Color : BaseEntity
    {
        public string Name { get; set; } = String.Empty;

        public virtual List<Car>? Cars { get; set; }
    }
}
