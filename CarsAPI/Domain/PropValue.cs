namespace Domain
{
    public class PropValue : BaseEntity
    {
        public string Value { get; set; } = string.Empty;


        public Guid PropertyId { get; set; }
        public virtual Property? Property { get; set; }

        public virtual List<Car_PropValue>? Car_PropValues { get; set; }
    }
}
