namespace Domain
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Name { get; set; }= String.Empty;

        public virtual List<Property_PropertyValue>? Property_PropertyValues { get; set; }
    }
}
