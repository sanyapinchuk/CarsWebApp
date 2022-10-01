namespace Domain
{
    public class Property_PropValue
    {
        public Guid Id { get; set; }

        public Guid PropertyId { get; set; }
        public virtual Property? Property { get; set; }

        public Guid PropValueId { get; set; }
        public virtual PropValue? PropValue { get; set; }


        public virtual List<Car__Property_PropValue>? Car__Property_PropValues { get; set; }

    }
}
