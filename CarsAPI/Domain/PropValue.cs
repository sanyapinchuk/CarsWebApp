namespace Domain
{
    public class PropValue
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;


        public Guid PropertyId { get; set; }
        public Property? Property { get; set; }

        public virtual List<Car_PropValue>? Car_PropValues { get; set; }
    }
}
