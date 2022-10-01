namespace Domain
{
    public class PropValue
    {
        public Guid Id { get; set; }
        public string Value { get; set; } = string.Empty;

        public virtual List<Property_PropertyValue>? Property_PropertyValues { get; set; }
    }
}
